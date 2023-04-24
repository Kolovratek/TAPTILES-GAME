using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Taptiles.Core;

namespace Taptiles.Core
{
    [Serializable]
    public class Field
    {
        public Tile[,] _tiles;
        public int Row { get; init; }
        public int Column { get; init; }

        private DateTime startTime;

        public Field(int row, int column)
        {
            Row = row;
            Column = column;
            _tiles = new Tile[Row, Column];
            Generate();
            startTime = DateTime.Now;
        }

        public Tile GetTile(int row, int column)
        {
            return _tiles[row, column];
        }

        public Tile this[int row, int column]
        {
            get { return _tiles[row, column]; }
        }

        private void Generate()
        {
            var random = new Random();
            var list = new List<Tile>();
            for (int i = 0; i < Row * Column; i++)
            {
                list.Add(new Tile(i / 4));
            }
            list = list.OrderBy(a => random.Next()).ToList();

            for (var row = 0; row < Row; row++)
            {
                var column = 0;
                for (column = 0; column < Column; column++)
                {
                    _tiles[row, column] = list.First();
                    list.RemoveAt(0);
                }
            }
        }

        public bool Win()
        {
            for (var row = 0; row < Row; row++)
            {
                for (var column = 0; column < Column; column++)
                {
                    if (_tiles[row, column].Value != 5)
                    {
                        Console.WriteLine("\nThe game continues");
                        return false;
                    }
                }
            }
            Console.WriteLine("\nYou Win");
            Console.WriteLine("\nCongratulations");

            var score = 60 - ((int)(DateTime.Now - startTime).TotalSeconds);
            if(score >= 0)
            {
                score = score;
            }
            else
            {
                score = 0;
            }
            Console.WriteLine("\nYour score is {0}", score);
            return true;

        }

        public bool IsSolved()
        {
            for (var row = 0; row < Row; row++)
            {
                for (var column = 0; column < Column; column++)
                {
                    var tile = _tiles[row, column];
                    if (tile.Value != 5)
                        return false;
                }
            }
            return true;
        }

        public int GetScore(DateTime startTime)
        {
            Debug.WriteLine(startTime);
            var score = 60 - ((int) (DateTime.Now - startTime).TotalSeconds);
            return score >= 0 ? score : 0;
        }
    }
}
