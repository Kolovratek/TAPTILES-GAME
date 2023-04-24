using Taptiles.Entity;

namespace Taptiles.Service
{
    public interface IScoreService
    {
        void AddScore(Score score);

        IList<Score> GetTopScores();

        IList<Score> GetAllScores();

        void ResetScore();
    }
}
