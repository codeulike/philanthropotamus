using SharpArch.Testing.NUnit.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using NUnit.Framework;
using Wwwtw.Domain;
using Wwwtw.Infrastructure;

namespace Wwwtw.Tests
{
    [TestFixture]
    public class CharityInfoRepositoryTests : RepositoryTestsBase 
    {
        protected override void LoadTestData()
        {
            ServiceLocatorInitializer.Init(); 
        }

        [Test]
        public void CanReturnSingleRecentItem()
        {

            // save a prod
            var ciRep = new NHibernateRepository<CharityInfo>();
            var ci = new CharityInfo();
            ci.CharityName = "testprod";
            ciRep.SaveOrUpdate(ci);
            int savedId = ci.Id;
            this.FlushSessionAndEvict(ci);

            var ciRep2 = new NHibernateRepository<CharityInfo>();
            var foundProd = ciRep2.Get(savedId);

            Assert.AreEqual("testprod", foundProd.CharityName);


        }

        [Test]
        public void CanReturnTopN()
        {

            // save a few items
            var ciRep = new CharityInfoRepository();
            var ci1 = new CharityInfo();
            ci1.CharityName = "test1";
            ci1.GameScore = 1400;
            ciRep.SaveOrUpdate(ci1);
            int saved1Id = ci1.Id;
            this.FlushSessionAndEvict(ci1);
            var ci2 = new CharityInfo();
            ci2.CharityName = "test2";
            ci2.GameScore = 1500;
            ciRep.SaveOrUpdate(ci2);
            int saved2Id = ci2.Id;
            this.FlushSessionAndEvict(ci2);
            var ci3 = new CharityInfo();
            ci3.CharityName = "test3";
            ci3.GameScore = 1100;
            ciRep.SaveOrUpdate(ci3);
            int saved3Id = ci3.Id;
            this.FlushSessionAndEvict(ci3);

            var ciRep2 = new CharityInfoRepository();
            var topList = ciRep2.GetTop(2);

            Assert.AreEqual(2, topList.Count());
            Assert.AreEqual("test2", topList[0].CharityName);
            Assert.AreEqual("test1", topList[1].CharityName);




        }
        
    }
}
