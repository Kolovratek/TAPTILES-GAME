using Taptiles.Entity;
using Microsoft.EntityFrameworkCore;

namespace Taptiles.Service
{
    public class RatingServiceEF : IRatingService
    {
        public void AddRating(Rating rating)
        {
            using (var context = new TaptilesDbContext())
            {
                context.Star.Add(rating);
                context.SaveChanges();
            }
        }

        public IList<Rating> GetLastRating()
        {
            using (var context = new TaptilesDbContext())
            {
                return (from s in context.Star orderby s.Stars descending select s).Take(3).ToList();
            }
        }

        public IList<Rating> GetAllRating()
        {
            using (var context = new TaptilesDbContext())
            {
                return (from s in context.Star orderby s.Stars descending select s).ToList();
            }
        }

        public void DeleteRating()
        {
            using (var context = new TaptilesDbContext())
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Rating");
            }
        }
    }
}
