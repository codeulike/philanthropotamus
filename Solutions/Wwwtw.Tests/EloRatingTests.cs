using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Wwwtw.Tasks;

namespace Wwwtw.Tests
{
    [TestFixture]
    public class EloRatingTests
    {

        [Test]
        public void EloRating_General()
        {
            var elo = new EloRating(1200, 1200, 400, 0);

            Assert.AreEqual(60.0d, elo.Point1);
            Assert.AreEqual(-60.0d, elo.Point2);

            Assert.AreEqual(1260.0d, elo.FinalResult1);
            Assert.AreEqual(1140.0d, elo.FinalResult2);

        }

        [Test]
        public void EloRating_GeneralReversed()
        {
            var elo = new EloRating(1200, 1200, 0, 400);

            Assert.AreEqual(-60.0d, elo.Point1);
            Assert.AreEqual(60.0d, elo.Point2);

            Assert.AreEqual(1140.0d, elo.FinalResult1);
            Assert.AreEqual(1260.0d, elo.FinalResult2);

        }

        [Test]
        public void EloRating_Different()
        {
            var elo = new EloRating(1400, 1000, 400, 0);

            Assert.AreEqual(11.0d, elo.Point1);
            Assert.AreEqual(-11.0d, elo.Point2);

            Assert.AreEqual(1411.0d, elo.FinalResult1);
            Assert.AreEqual(989.0d, elo.FinalResult2);

        }

        [Test]
        public void EloRating_DifferentReverse()
        {
            var elo = new EloRating(1400, 1000, 0, 400);

            Assert.AreEqual(-109.0d, elo.Point1);
            Assert.AreEqual(109.0d, elo.Point2);

            Assert.AreEqual(1291.0d, elo.FinalResult1);
            Assert.AreEqual(1109.0d, elo.FinalResult2);

        }



    }
}
