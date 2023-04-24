using Microsoft.AspNetCore.Mvc;
using Taptiles.Entity;
using Taptiles.Service;

namespace TaptilesWeb.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private IRatingService _ratingService = new RatingServiceEF();

        [HttpGet]
        public IEnumerable<Rating> GetRating()
        {
            return _ratingService.GetLastRating();
        }

        [HttpPost]
        public void PostRating(Rating rating)
        {
            rating.PlayedAt = DateTime.Now;
            _ratingService.AddRating(rating);
        }
    }
}
