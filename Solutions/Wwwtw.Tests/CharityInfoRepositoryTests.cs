using SharpArch.Testing.NUnit.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using NUnit.Framework;
using Wwwtw.Domain;

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
    }
}
