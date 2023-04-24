using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taptiles.Service;
using Taptiles.Entity;
using System;

namespace TaptilesTest
{
    [TestClass]
    public class ScoreServiceTest
    {

        [TestMethod]
        public void AddTest1()
        {
            var service = CreateService();
            service.AddScore(new Score { Player = "Jaro", Points = 100, PlayedAt = DateTime.Now });

            Assert.AreEqual(1, service.GetTopScores().Count);
            Assert.AreEqual("Jaro", service.GetTopScores()[0].Player);
            Assert.AreEqual(100, service.GetTopScores()[0].Points);
        }

        [TestMethod]
        public void AddTest3()
        {
            var service = CreateService();
            service.AddScore(new Score { Player = "Jaro", Points = 100, PlayedAt = DateTime.Now });
            service.AddScore(new Score { Player = "Peter", Points = 200, PlayedAt = DateTime.Now });
            service.AddScore(new Score { Player = "Jozo", Points = 50, PlayedAt = DateTime.Now });

            Assert.AreEqual(3, service.GetTopScores().Count);

            Assert.AreEqual("Peter", service.GetTopScores()[0].Player);
            Assert.AreEqual(200, service.GetTopScores()[0].Points);

            Assert.AreEqual("Jaro", service.GetTopScores()[1].Player);
            Assert.AreEqual(100, service.GetTopScores()[1].Points);

            Assert.AreEqual("Jozo", service.GetTopScores()[2].Player);
            Assert.AreEqual(50, service.GetTopScores()[2].Points);
        }

        [TestMethod]
        public void ResetTest()
        {
            var service = CreateService();

            service.AddScore(new Score { Player = "Jaro", Points = 100, PlayedAt = DateTime.Now });
            service.AddScore(new Score { Player = "Jozo", Points = 200, PlayedAt = DateTime.Now });

            service.ResetScore();

            Assert.AreEqual(0, service.GetTopScores().Count);

        }
        private IScoreService CreateService()
        {
            return new ScoreServiceFile();
        }
    }
}