using System.Runtime.Serialization.Formatters.Binary;
using Taptiles.Entity;  

namespace Taptiles.Service
{
    public class ScoreServiceFile : IScoreService
    {
        private const string FileName = "score2.bin";

        private List<Score> _scores = new List<Score>();
        public void AddScore(Score score)
        {
            _scores.Add(score);
            SaveScore();
        }

        public IList<Score> GetTopScores()
        {
            LoadScore();
            return _scores.OrderByDescending(s => s.Points).Take(3).ToList(); 
        }

        public void ResetScore()
        {
            _scores.Clear();
            File.Delete(FileName);
        }

        private void SaveScore()
        {
            using (var fp = File.OpenWrite(FileName))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fp, _scores);
            }
        }

        private void LoadScore()
        {
            if (File.Exists(FileName))
            {
                using (var fp = File.OpenRead(FileName))
                {
                    var bf = new BinaryFormatter();
                    _scores = (List<Score>)bf.Deserialize(fp);
                }
            }
        }

        public IList<Score> GetAllScores()
        {
            LoadScore();
            return _scores.OrderByDescending(s => s.Points).ToList();
        }
    }
}
