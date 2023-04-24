using System;
using Taptiles.Core;
using Taptiles.Entity;
using Taptiles.Service;

namespace Taptiles.ConsoleUI
{
    public class ConsoleUI
    {
        private readonly Field _field;

        //private readonly IScoreService _scoreService = new ScoreServiceFile();
        private readonly IScoreService _scoreService = new ScoreServiceEF();
        private readonly IRatingService _ratingService = new RatingServiceEF();
        private readonly ICommentService _commentService = new CommentServiceEF();

        public ConsoleUI(Field field)
        {
            _field = field;
        }

        public void Play()
        {
            Console.Write("Enter your name: ");
            var playerName = Console.ReadLine();
            PrintTopScores();
            do
            {
                PrintField();
                Input();
            } while (!_field.Win());

            _scoreService.AddScore(new Score { Player = playerName, Points = _field.GetScore(), PlayedAt = DateTime.Now });
            Console.WriteLine("You want rating or comment this game?");
            Console.WriteLine("If yes press R for rating C for comment.");
            Console.Write("If no press what you want: ");
            var chose = Console.ReadLine();
            if (chose == "R" || chose == "r")
            {
                Console.WriteLine("Enter your rating using *");
                Console.WriteLine("You can use 1 - 5");
                Console.Write("Rating: ");
                var rating = Console.ReadLine();
                while(rating != "1" || rating != "2" || rating != "3" || rating != "4" || rating != "5")
                {
                    Console.Write("Rating: ");
                    rating = Console.ReadLine();
                }
                _ratingService.AddRating(new Rating { Player = playerName, Stars = rating, PlayedAt = DateTime.Now });
            }
            else if (chose == "C" || chose == "c")
            {
                Console.Write("Enter your comment: ");
                var comment = Console.ReadLine();
                _commentService.AddComment(new Coment { Player = playerName, Comment = comment, PlayedAt = DateTime.Now });
            }
            else
            {
                Console.WriteLine("Good bye");
            }
        }

        private void PrintField()
        {
            for (var row = 0; row < _field.Row; row++)
            {
                for (var column = 0; column < _field.Column; column++)
                {
                    var tile = _field[row, column];
                    if (tile != null && tile.Value != 5)
                    {
                        Console.Write("{0,3}", tile.Value);
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Score: {0,2}", _field.GetScore());
        }


        public void Input()
        {

            Console.WriteLine("\nEnter first row: ");
            int row1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter first column: ");
            int column1 = int.Parse(Console.ReadLine());

            if (row1 >= 4 || column1 >= 4)
            {
                Console.WriteLine("\nThe field size is smaller than your entered value");
                return;
            }

            var tile = _field[row1, column1];

            if (tile.Value == 5)
            {
                Console.WriteLine("\nYou deleted this tile before");
                return;
            }

            Console.WriteLine("\nYou entered row {0} and column {1}, and the value of this tile is {2}", row1, column1, tile.Value);

            Console.WriteLine("\nEnter second row: ");
            int row2 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter second column: ");
            int column2 = int.Parse(Console.ReadLine());

            if (row2 >= 4 || column2 >= 4)
            {
                Console.WriteLine("\nThe field size is smaller than your entered value");
                return;
            }

            var tile2 = _field[row2, column2];

            if (tile2.Value == 5)
            {
                Console.WriteLine("\nYou deleted this tile before");
                return;
            }

            Console.WriteLine("\nYou entered row {0} and column {1}, and the value of this tile is {2}", row2, column2, tile2.Value);

            if (tile.Value != tile2.Value)
            {
                Console.WriteLine("\nYou entered different field values");
                return;
            }

            if (row1 == row2 && column1 == column2)
            {
                Console.WriteLine("\nYou entered same tile");
                Console.WriteLine("We cant delete this");
                return;
            }

            Functionality.Delete(_field, row1, column1, row2, column2);

        }

        private void PrintTopScores()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("---------------------- TOP SCORES ------------------------");
            Console.WriteLine("----------------------------------------------------------");
            foreach (var score in _scoreService.GetTopScores())
            {
                Console.WriteLine("{0} {1}", score.Player, score.Points);
            }
            Console.WriteLine("----------------------------------------------------------");
        }
    }
}
