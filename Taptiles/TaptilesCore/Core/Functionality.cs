namespace Taptiles.Core
{
    public static class Functionality
    {
        public static void Delete(Field f, int row1, int column1, int row2, int column2)
        {

            var tile = f[row1, column1];
            var tile2 = f[row2, column2];

            if (tile.Value != tile2.Value)
            {
                return;
            }

            Func<int, int, int, bool> areEqual = (x, y, val) => x == val && y == val;

            bool isFirstRow = areEqual(row1, row2, 0);
            bool isLastRow = areEqual(row1, row2, 3);
            bool isFirstCol = areEqual(column1, column2, 0);
            bool isLastCol = areEqual(column1, column2, 3);

            //Krajne riadky a stlpce
            bool isSomeFirstOrLast = isFirstRow || isLastRow || isFirstCol || isLastCol;

            bool areEqualCols = column1 == column2;
            bool areEqualRows = row1 == row2;

            //Mazanie hned vedla seba
            bool goRight = row1 < 3 && row2 == row1 + 1;
            bool goLeft = row1 > 0 && row2 == row1 - 1;
            bool goDown = column1 < 3 && column2 == column1 + 1;
            bool goUp = column1 > 0 && column2 == column1 - 1;

            //Hned vedla seba
            bool goRightLeft = (goRight || goLeft) && areEqualCols;
            bool goUpDown = (goUp || goDown) && areEqualRows;

            //Vzdialene policka +- 2-3 v riadku
            bool isDiffColTwo = column1 - column2 == 2;
            bool isDiffColTwoRev = column2 - column1 == 2;
            bool isDiffColThree = column1 - column2 == 3;
            bool isDiffColThreeRev = column2 - column1 == 3;

            //Vzdialene policka +- 2-3 v stlpci
            bool isDiffRowTwo = row1 - row2 == 2;
            bool isDiffRowTwoRev = row2 - row1 == 2;
            bool isDiffRowThree = row1 - row2 == 3;
            bool isDiffRowThreeRev = row2 - row1 == 3;

            //Mozem zmazat v rovnakom riadku vzdialene policka
            bool canDeleteInSameRow1 = isDiffColTwo && IsEmptyTile(f[row1, column1 - 1]);
            bool canDeleteInSameRow2 = isDiffColTwoRev && IsEmptyTile(f[row1, column1 + 1]);
            bool canDeleteInSameRow3 = isDiffColThree && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2]);
            bool canDeleteInSameRow4 = isDiffColThreeRev && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2]);

            //Mozem zmazat v rovnakom stlpci vzdialene policka
            bool canDeleteInSameCol1 = isDiffRowTwo && IsEmptyTile(f[row1 - 1, column1]);
            bool canDeleteInSameCol2 = isDiffRowTwoRev && IsEmptyTile(f[row1 + 1, column1]);
            bool canDeleteInSameCol3 = isDiffRowThree && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1]);
            bool canDeleteInSameCol4 = isDiffRowThreeRev && IsEmptyTile(f[row1 + 1, column1],f[row1 + 2, column1]);

            //V jednom riadku a su od seba vzdialene 2-3 miesta
            bool sameRowTwoTreePlaces = areEqualRows && (canDeleteInSameRow1 || canDeleteInSameRow2 || canDeleteInSameRow3 || canDeleteInSameRow4);

            //V jednom stlpci a su od seba vzdialene 2-3 miesta
            bool sameColTwoTreePlaces = areEqualCols && (canDeleteInSameCol1 || canDeleteInSameCol2 || canDeleteInSameCol3 || canDeleteInSameCol4);

            //Pole ktoré chcem spojiť so svojím je doprava +1+2+3
            bool ColPlusOne = (column2 == column1 + 1);
            bool ColPlusTwo = (column2 == column1 + 2);
            bool ColPlusThree = (column2 == column1 + 3);

            //Pole ktoré chcem spojiť so svojím je dolava -1-2-3
            bool ColMinusOne = (column2 == column1 - 1);
            bool ColMinusTwo = (column2 == column1 - 2);
            bool ColMinusThree = (column2 == column1 - 3);

            //Pole ktoré chcem spojiť so svojím je dole +1+2+3
            bool RowPlusOne = row2 == row1 + 1;
            bool RowPlusTwo = row2 == row1 + 2;
            bool RowPlusThree = row2 == row1 + 3;

            //Pole ktoré chcem spojiť so svojím je hore -1-2-3
            bool RowMinusOne = row2 == row1 - 1;
            bool RowMinusTwo = row2 == row1 - 2;
            bool RowMinusThree = row2 == row1 - 3;


            //Mazanie v hre
            if (isSomeFirstOrLast || goRightLeft || goUpDown || sameRowTwoTreePlaces || sameColTwoTreePlaces)
            {
                tile.Value = 5;
                tile2.Value = 5;
            }
            else if (row1 != row2 && column1 != column2)
            {
                if (RowPlusOne)
                {
                    //Riadok nizsie do prava+1
                    bool ColPlusOneIsFreeRoad = ColPlusOne && IsEmptyTile(f[row1 + 1, column1]);
                    bool ColPlusOneIsFreeRoadRev = ColPlusOne && IsEmptyTile(f[row1, column1 + 1]);

                    //Riadok nizsie do prava+2
                    bool ColPlusTwoIsFreeRoad = ColPlusTwo && IsEmptyTile(f[row1 + 1, column1], f[row1 + 1, column1 + 1]);
                    bool ColPlusTwoIsFreeRoadRev = ColPlusTwo && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2]);

                    //Riadok nizsie do prava+3
                    bool ColPlusThreeIsFreeRoad = ColPlusThree && IsEmptyTile(f[row1 + 1, column1], f[row1 + 1, column1 + 1], f[row1 + 1, column1 + 2]);
                    bool ColPlusThreeIsFreeRoadRev = ColPlusThree && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2], f[row1, column1 + 3]);

                    //Riadok nizsie do lava -1
                    bool ColMinusOneIsFreeRoad = ColMinusOne && IsEmptyTile(f[row1 + 1, column1]);
                    bool ColMinusOneIsFreeRoadRev = ColMinusOne && IsEmptyTile(f[row1, column1 - 1]);

                    //Riadok nizsie do lava -2
                    bool ColMinusTwoIsFreeRoad = ColMinusTwo && IsEmptyTile(f[row1 + 1, column1], f[row1 + 1, column1 - 1]);
                    bool ColMinusTwoIsFreeRoadRev = ColMinusTwo && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2]);

                    //Riadok nizsie do lava-3
                    bool ColMinusThreeIsFreeRoad = ColMinusThree && IsEmptyTile(f[row1 + 1, column1], f[row1 + 1, column1 - 1], f[row1 + 1, column1 - 2]);
                    bool ColMinusThreeIsFreeRoadRev = ColMinusThree && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2], f[row1, column1 - 3]);

                    if (ColPlusOneIsFreeRoad || ColPlusOneIsFreeRoadRev || ColPlusTwoIsFreeRoad || ColPlusTwoIsFreeRoadRev
                        || ColPlusThreeIsFreeRoad || ColPlusThreeIsFreeRoadRev || ColMinusOneIsFreeRoad || ColMinusOneIsFreeRoadRev
                        || ColMinusTwoIsFreeRoad || ColMinusTwoIsFreeRoadRev || ColMinusThreeIsFreeRoad || ColMinusThreeIsFreeRoadRev)
                    {
                        tile.Value = 5;
                        tile2.Value = 5;
                    }
                }
                else if (RowPlusTwo)
                {
                    //2 Riadky nizsie do prava+1
                    bool ColPlusOneIsFreeRoad2 = ColPlusOne && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1]);
                    bool ColPlusOneIsFreeRoad2Rev = ColPlusOne && IsEmptyTile(f[row1, column1 + 1], f[row1 + 1, column1 + 1]);

                    //2 Riadky nizsie do prava+2
                    bool ColPlusTwoIsFreeRoad2 = ColPlusTwo && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1], f[row1 + 2, column1 + 1]);
                    bool ColPlusTwoIsFreeRoad2Rev = ColPlusTwo && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2], f[row1 + 1, column1 + 2]);

                    //2 Riadky nizsie do prava+3
                    bool ColPlusThreeIsFreeRoad2 = ColPlusThree && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1], f[row1 + 2, column1 + 1], f[row1 + 2, column1 + 2]);
                    bool ColPlusThreeIsFreeRoad2Rev = ColPlusThree && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2], f[row1, column1 + 3], f[row1 + 1, column1 + 3]);

                    //2 Riadky nizsie do lava-1
                    bool ColMinusOneIsFreeRoad2 = ColMinusOne && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1]);
                    bool ColMinusOneIsFreeRoad2Rev = ColMinusOne && IsEmptyTile(f[row1, column1 - 1], f[row1 + 1, column1 - 1]);

                    //2 Riadky nizsie do lava-2
                    bool ColMinusTwoIsFreeRoad2 = ColMinusTwo && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1], f[row1 + 2, column1 - 1]);
                    bool ColMinusTwoIsFreeRoad2Rev = ColMinusTwo && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2], f[row1 + 1, column1 - 2]);

                    //2 Riadky nizsie do lava-3
                    bool ColMinusThreeIsFreeRoad2 = ColMinusThree && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1], f[row1 + 2, column1 - 1], f[row1 + 2, column1 - 2]);
                    bool ColMinusThreeIsFreeRoad2Rev = ColMinusThree && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2], f[row1, column1 - 3], f[row1 + 1, column1 - 3]);

                    if (ColPlusOneIsFreeRoad2 || ColPlusOneIsFreeRoad2Rev || ColPlusTwoIsFreeRoad2 || ColPlusTwoIsFreeRoad2Rev
                        || ColPlusThreeIsFreeRoad2 || ColPlusThreeIsFreeRoad2Rev || ColMinusOneIsFreeRoad2 || ColMinusOneIsFreeRoad2Rev
                        || ColMinusTwoIsFreeRoad2 || ColMinusTwoIsFreeRoad2Rev || ColMinusThreeIsFreeRoad2 || ColMinusThreeIsFreeRoad2Rev)
                    {
                        tile.Value = 5;
                        tile2.Value = 5;
                    }
                }
                else if (RowPlusThree)
                {
                    //3 Riadky nizsie do prava+1
                    bool ColPlusOneIsFreeRoad3 = ColPlusOne && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1], f[row1 + 3, column1]);
                    bool ColPlusOneIsFreeRoad3Rev = ColPlusOne && IsEmptyTile(f[row1, column1 + 1], f[row1 + 1, column1 + 1], f[row1 + 2, column1 + 1]);

                    //3 Riadky nizsie do prava+2
                    bool ColPlusTwoIsFreeRoad3 = ColPlusTwo && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1], f[row1 + 3, column1], f[row1 + 3, column1 + 1]);
                    bool ColPlusTwoIsFreeRoad3Rev = ColPlusTwo && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2], f[row1 + 1, column1 + 2], f[row1 + 2, column1 + 2]);

                    //3 Riadky nizsie do prava+3
                    bool ColPlusThreeIsFreeRoad3 = ColPlusThree && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1], f[row1 + 3, column1], f[row1 + 3, column1 + 1], f[row1 + 3, column1 + 2]);
                    bool ColPlusThreeIsFreeRoad3Rev = ColPlusThree && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2], f[row1, column1 + 3], f[row1 + 1, column1 + 3], f[row1 + 2, column1 + 3]);

                    //3 Riadky nizsie do lava-1
                    bool ColMinusOneIsFreeRoad3 = ColMinusOne && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1], f[row1 + 3, column1]);
                    bool ColMinusOneIsFreeRoad3Rev = ColMinusOne && IsEmptyTile(f[row1, column1 - 1], f[row1 + 1, column1 - 1], f[row1 + 2, column1 - 1]);

                    //3 Riadky nizsie do lava-2
                    bool ColMinusTwoIsFreeRoad3 = ColMinusTwo && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1], f[row1 + 3, column1], f[row1 + 3, column1 - 1]);
                    bool ColMinusTwoIsFreeRoad3Rev = ColMinusTwo && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2], f[row1 + 1, column1 - 2], f[row1 + 2, column1 - 2]);

                    //3 Riadky nizsie do lava-3
                    bool ColMinusThreeIsFreeRoad3 = ColMinusThree && IsEmptyTile(f[row1 + 1, column1], f[row1 + 2, column1], f[row1 + 3, column1], f[row1 + 3, column1 - 1], f[row1 + 3, column1 - 2]);
                    bool ColMinusThreeIsFreeRoad3Rev = ColMinusThree && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2], f[row1, column1 - 3], f[row1 + 1, column1 - 3], f[row1 + 2, column1 - 3]);


                    if (ColPlusOneIsFreeRoad3 || ColPlusOneIsFreeRoad3Rev || ColPlusTwoIsFreeRoad3 || ColPlusTwoIsFreeRoad3Rev
                        || ColPlusThreeIsFreeRoad3 || ColPlusThreeIsFreeRoad3Rev || ColMinusOneIsFreeRoad3 || ColMinusOneIsFreeRoad3Rev
                        || ColMinusTwoIsFreeRoad3 || ColMinusTwoIsFreeRoad3Rev || ColMinusThreeIsFreeRoad3 || ColMinusThreeIsFreeRoad3Rev)
                    {
                        tile.Value = 5;
                        tile2.Value = 5;
                    }
                }
                //------------------------------------------------------------------
                else if (RowMinusOne)
                {
                    //Riadok vyssie do prava+1
                    bool ColPlusOneIsFreeRoad = ColPlusOne && IsEmptyTile(f[row1 - 1, column1]);
                    bool ColPlusOneIsFreeRoadRev = ColPlusOne && IsEmptyTile(f[row1, column1 + 1]);

                    //Riadok vyssie do prava+2
                    bool ColPlusTwoIsFreeRoad = ColPlusTwo && IsEmptyTile(f[row1 - 1, column1], f[row1 - 1, column1 + 1]);
                    bool ColPlusTwoIsFreeRoadRev = ColPlusTwo && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2]);

                    //Riadok vyssie do prava+3
                    bool ColPlusThreeIsFreeRoad = ColPlusThree && IsEmptyTile(f[row1 - 1, column1], f[row1 - 1, column1 + 1], f[row1 - 1, column1 + 2]);
                    bool ColPlusThreeIsFreeRoadRev = ColPlusThree && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2], f[row1, column1 + 3]);

                    //Riadok vyssie do lava-1
                    bool ColMinusOneIsFreeRoad = ColMinusOne && IsEmptyTile(f[row1 - 1, column1]);
                    bool ColMinusOneIsFreeRoadRev = ColMinusOne && IsEmptyTile(f[row1, column1 - 1]);

                    //Riadok vyssie do lava-2
                    bool ColMinusTwoIsFreeRoad = ColMinusTwo && IsEmptyTile(f[row1 - 1, column1], f[row1 - 1, column1 - 1]);
                    bool ColMinusTwoIsFreeRoadRev = ColMinusTwo && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2]);

                    //Riadok vyssie do lava-3
                    bool ColMinusThreeIsFreeRoad = ColMinusThree && IsEmptyTile(f[row1 - 1, column1], f[row1 - 1, column1 - 1], f[row1 - 1, column1 - 2]);
                    bool ColMinusThreeIsFreeRoadRev = ColMinusThree && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2], f[row1, column1 - 3]);

                    if (ColPlusOneIsFreeRoad || ColPlusOneIsFreeRoadRev || ColPlusTwoIsFreeRoad || ColPlusTwoIsFreeRoadRev
                        || ColPlusThreeIsFreeRoad || ColPlusThreeIsFreeRoadRev || ColMinusOneIsFreeRoad || ColMinusOneIsFreeRoadRev
                        || ColMinusTwoIsFreeRoad || ColMinusTwoIsFreeRoadRev || ColMinusThreeIsFreeRoad || ColMinusThreeIsFreeRoadRev)
                    {
                        tile.Value = 5;
                        tile2.Value = 5;
                    }
                }
                else if (RowMinusTwo)
                {
                    //2 Riadky vyssie do prava+1
                    bool ColPlusOneIsFreeRoad2 = ColPlusOne && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1]);
                    bool ColPlusOneIsFreeRoad2Rev = ColPlusOne && IsEmptyTile(f[row1, column1 + 1], f[row1 - 1, column1 + 1]);

                    //2 Riadky vyssie do prava+2
                    bool ColPlusTwoIsFreeRoad2 = ColPlusTwo && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1], f[row1 - 2, column1 + 1]);
                    bool ColPlusTwoIsFreeRoad2Rev = ColPlusTwo && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2], f[row1 - 1, column1 + 2]);

                    //2 Riadky vyssie do prava+3
                    bool ColPlusThreeIsFreeRoad2 = ColPlusThree && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1], f[row1 - 2, column1 + 1], f[row1 - 2, column1 + 2]);
                    bool ColPlusThreeIsFreeRoad2Rev = ColPlusThree && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2], f[row1, column1 + 3], f[row1 - 1, column1 + 3]);

                    //2 Riadky vyssie do lava-1
                    bool ColMinusOneIsFreeRoad2 = ColMinusOne && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1]);
                    bool ColMinusOneIsFreeRoad2Rev = ColMinusOne && IsEmptyTile(f[row1, column1 - 1], f[row1 - 1, column1 - 1]);

                    //2 Riadky vyssie do lava-2
                    bool ColMinusTwoIsFreeRoad2 = ColMinusTwo && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1], f[row1 - 2, column1 - 1]);
                    bool ColMinusTwoIsFreeRoad2Rev = ColMinusTwo && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2], f[row1 - 1, column1 - 2]);

                    //2 Riadky vyssie do lava-3
                    bool ColMinusThreeIsFreeRoad2 = ColMinusThree && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1], f[row1 - 2, column1 - 1], f[row1 - 2, column1 - 2]);
                    bool ColMinusThreeIsFreeRoad2Rev = ColMinusThree && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2], f[row1, column1 - 3], f[row1 - 1, column1 - 3]);

                    if (ColPlusOneIsFreeRoad2 || ColPlusOneIsFreeRoad2Rev || ColPlusTwoIsFreeRoad2 || ColPlusTwoIsFreeRoad2Rev
                        || ColPlusThreeIsFreeRoad2 || ColPlusThreeIsFreeRoad2Rev || ColMinusOneIsFreeRoad2 || ColMinusOneIsFreeRoad2Rev
                        || ColMinusTwoIsFreeRoad2 || ColMinusTwoIsFreeRoad2Rev || ColMinusThreeIsFreeRoad2 || ColMinusThreeIsFreeRoad2Rev)
                    {
                        tile.Value = 5;
                        tile2.Value = 5;
                    }
                }
                else if (RowMinusThree)
                {
                    //3 Riadky vyssie do prava+1
                    bool ColPlusOneIsFreeRoad3 = ColPlusOne && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1], f[row1 - 3, column1]);
                    bool ColPlusOneIsFreeRoad3Rev = ColPlusOne && IsEmptyTile(f[row1, column1 + 1], f[row1 - 1, column1 + 1], f[row1 - 2, column1 + 1]);

                    //3 Riadky vyssie do prava+2
                    bool ColPlusTwoIsFreeRoad3 = ColPlusTwo && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1], f[row1 - 3, column1], f[row1 - 3, column1 + 1]);
                    bool ColPlusTwoIsFreeRoad3Rev = ColPlusTwo && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2], f[row1 - 1, column1 + 2], f[row1 - 2, column1 + 2]);

                    //3 Riadky vyssie do prava+3
                    bool ColPlusThreeIsFreeRoad3 = ColPlusThree && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1], f[row1 - 3, column1], f[row1 - 3, column1 + 1], f[row1 - 3, column1 + 2]);
                    bool ColPlusThreeIsFreeRoad3Rev = ColPlusThree && IsEmptyTile(f[row1, column1 + 1], f[row1, column1 + 2], f[row1, column1 + 3], f[row1 - 1, column1 + 3], f[row1 - 2, column1 + 3]);

                    //3 Riadky vyssie do lava-1
                    bool ColMinusOneIsFreeRoad3 = ColMinusOne && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1], f[row1 - 3, column1]);
                    bool ColMinusOneIsFreeRoad3Rev = ColMinusOne && IsEmptyTile(f[row1, column1 - 1], f[row1 - 1, column1 - 1], f[row1 - 2, column1 - 1]);

                    //3 Riadky vyssie do lava-2
                    bool ColMinusTwoIsFreeRoad3 = ColMinusTwo && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1], f[row1 - 3, column1], f[row1 - 3, column1 - 1]);
                    bool ColMinusTwoIsFreeRoad3Rev = ColMinusTwo && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2], f[row1 - 1, column1 - 2], f[row1 - 2, column1 - 2]);

                    //3 Riadky vyssie do lava-3
                    bool ColMinusThreeIsFreeRoad3 = ColMinusThree && IsEmptyTile(f[row1 - 1, column1], f[row1 - 2, column1], f[row1 - 3, column1], f[row1 - 3, column1 - 1], f[row1 - 3, column1 - 2]);
                    bool ColMinusThreeIsFreeRoad3Rev = ColMinusThree && IsEmptyTile(f[row1, column1 - 1], f[row1, column1 - 2], f[row1, column1 - 3], f[row1 - 1, column1 - 3], f[row1 - 2, column1 - 3]);

                    if (ColPlusOneIsFreeRoad3 || ColPlusOneIsFreeRoad3Rev || ColPlusTwoIsFreeRoad3 || ColPlusTwoIsFreeRoad3Rev
                        || ColPlusThreeIsFreeRoad3 || ColPlusThreeIsFreeRoad3Rev || ColMinusOneIsFreeRoad3 || ColMinusOneIsFreeRoad3Rev
                        || ColMinusTwoIsFreeRoad3 || ColMinusTwoIsFreeRoad3Rev || ColMinusThreeIsFreeRoad3 || ColMinusThreeIsFreeRoad3Rev)
                    {
                        tile.Value = 5;
                        tile2.Value = 5;
                    }
                }
            }
            else
            {
                Console.WriteLine("\nWe cant delete this");
            }
        }
        private static bool IsEmptyTile(params Tile[] tiles)
        {
            if(tiles == null) { return true; }
            var result = true;
            foreach (var item in tiles)
            {
                result = result && item.Value == 5;
            }
            return result;
        }
    }

}
