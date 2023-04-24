using Taptiles.Entity;
using Microsoft.EntityFrameworkCore;

namespace Taptiles.Service
{
    public class ScoreServiceEF : IScoreService 
    {
        public void AddScore(Score score)
        {
            using (var context = new TaptilesDbContext())
            {
                context.Scores.Add(score);
                context.SaveChanges();
            }
        }

        public IList<Score> GetTopScores()
        {
            using (var context = new TaptilesDbContext())
            {
                return(from s in context.Scores orderby s.Points descending select s).Take(3).ToList();
            }
        }

        public IList<Score> GetAllScores()
        {
            using (var context = new TaptilesDbContext())
            {
                return (from s in context.Scores orderby s.Points descending select s).ToList();
            }
        }

        public void ResetScore()
        {
            using (var context = new TaptilesDbContext())
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Scores");
            }
        }
    }
}
