using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taptiles.Entity;
using Taptiles.Service;

namespace TaptilesWeb.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private IScoreService _scoreService = new ScoreServiceEF();

        [HttpGet]
        public IEnumerable<Score> GetScores()
        {
            return _scoreService.GetTopScores();
        }

        [HttpPost]
        public void PostScore(Score score)
        {
            score.PlayedAt = DateTime.Now;
            _scoreService.AddScore(score);
        }
    }
}
