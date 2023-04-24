using Microsoft.AspNetCore.Mvc;
using Taptiles.Core;
using Microsoft.AspNetCore.Http;
using Taptiles.Service;
using TaptilesWeb.Models;
using Taptiles.Entity;
using System.Diagnostics;


namespace TaptilesWeb.Controllers
{
    public class TaptilesController : Controller
    {
        private readonly GameTimer _timer;

        public TaptilesController(GameTimer timer) : base()
        {
            _timer = timer;
        }

        const string FieldSessionKey = "field";

        private IScoreService _scoreService = new ScoreServiceEF();

        private ICommentService _commentService = new CommentServiceEF();

        private IRatingService _ratingService = new RatingServiceEF();
        
        public IActionResult Index()
        {
            var field = new Field(4, 4);

            _timer.startTime = DateTime.Now;
            //Debug.WriteLine($"{DateTime.Now.ToString()} generating code {field}", "app");
            HttpContext.Session.SetObject(FieldSessionKey, field);
            
            return View("Index", CreateModel());
        }

        public IActionResult Delete([FromQuery] int row1, [FromQuery] int column1, [FromQuery] int row2, [FromQuery] int column2)
        {
            var field = HttpContext.Session.GetObject<Field>(FieldSessionKey);
            Functionality.Delete(field, row1, column1, row2, column2);
            HttpContext.Session.SetObject(FieldSessionKey, field);

            return View("Index", CreateModel());
        }

        public IActionResult AddName(string Player)
        {
            var field = HttpContext.Session.GetObject<Field>(FieldSessionKey);
            if (field.IsSolved())
            {
                _scoreService.AddScore(new Score() { PlayedAt = DateTime.Now, Player = Player, Points = field.GetScore(_timer.startTime) });
            }
            return View("Index", CreateModel());
        }

        public IActionResult AddComment(string Player, string Coment)
        {
            _commentService.AddComment(new Coment() { PlayedAt = DateTime.Now, Player = Player, Comment = Coment });

            return View("Index", CreateModel());
        }

        public IActionResult AddRating(string Player, string Stars)
        {
            _ratingService.AddRating(new Rating() { PlayedAt = DateTime.Now, Player = Player, Stars = Stars });

            return View("Index", CreateModel());
        }

        private TaptilesModel CreateModel()
        {
            var field = HttpContext.Session.GetObject<Field>(FieldSessionKey);
            var scores = _scoreService.GetTopScores();
            var coment = _commentService.GetAllComment();
            var stars = _ratingService.GetAllRating();

            return new TaptilesModel { Field = field, Scores = scores, Coments = coment, Ratings = stars };
        }
    }

    public class ScoreController : Controller
    {
        private IScoreService _scoreService = new ScoreServiceEF();
        public IActionResult Index()
        {
            CreateModel();

            return View("Index", CreateModel());
        }

        private TaptilesModel CreateModel()
        {
            var score = _scoreService.GetAllScores();

            return new TaptilesModel { Scores = score };
        }
    }

    public class CommentController : Controller
    {
        private ICommentService _commentService = new CommentServiceEF();
        public IActionResult Index()
        {
            CreateModel();

            return View("Index", CreateModel());
        }

        private TaptilesModel CreateModel()
        {
            var comment = _commentService.GetAllComment();

            return new TaptilesModel { Coments = comment };
        }
    }

    public class RatingController : Controller
    {
        private IRatingService _ratingService = new RatingServiceEF();
        public IActionResult Index()
        {
            CreateModel();

            return View("Index", CreateModel());
        }

        private TaptilesModel CreateModel()
        {
            var rating = _ratingService.GetAllRating();

            return new TaptilesModel { Ratings = rating };
        }
    }

    public class WinController : Controller
    {
        private IRatingService _ratingService = new RatingServiceEF();
        public IActionResult Index()
        {
            CreateModel();

            return View("Index", CreateModel());
        }

        private TaptilesModel CreateModel()
        {

            return new TaptilesModel {};
        }
    }


}
