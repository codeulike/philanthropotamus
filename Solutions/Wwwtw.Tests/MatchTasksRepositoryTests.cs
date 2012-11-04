using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Testing.NUnit.NHibernate;
using SharpArch.NHibernate;
using NUnit.Framework;
using Wwwtw.Domain;
using Wwwtw.Tasks;
using Wwwtw.Infrastructure;


namespace Wwwtw.Tests
{
    [TestFixture]
    public class MatchTasksRepositoryTests : RepositoryTestsBase
    {
        protected override void LoadTestData()
        {
            ServiceLocatorInitializer.Init();
            MatchTasks.ClearMinMax();
        }


        private Tuple<int,int> MakeSomeData(int howMany)
        {
            int firstId = -1;
            int lastId = -1;

            var ciRep = new NHibernateRepository<CharityInfo>();
            for (int i = 0; i <= howMany; i++)
            {
                var ci = new CharityInfo();
                ci.CharityName = "testprod"+ i.ToString();
                ciRep.SaveOrUpdate(ci);
                
                if (firstId == -1)
                    firstId = ci.Id;
                if (i == howMany)
                    lastId = ci.Id;
                this.FlushSessionAndEvict(ci);
            }

            return new Tuple<int, int>(firstId, lastId);
        }

        [Test]
        public void FetchUntilNot_FailsAfter10()
        {

            // save a ci
            var ciRep = new CharityInfoRepository();
            var ci = new CharityInfo();
            ci.CharityName = "testprod";
            ciRep.SaveOrUpdate(ci);
            int savedId = ci.Id;
            this.FlushSessionAndEvict(ci);

            var ciRep2 = new CharityInfoRepository();
            var tasks = new MatchTasks(ciRep2);

            bool expectedErrorFound = false;
            try
            {
                var randomCi = tasks.FetchUntilNot(0, savedId + 1, savedId + 11);
            }
            catch (ApplicationException appex)
            {
                if (appex.Message.StartsWith("FetchUntilNot failed to find a random entity after"))
                    expectedErrorFound = true;
            }
            Assert.IsTrue(expectedErrorFound);


        }

        [Test]
        public void FetchUntilNot_CanFetch()
        {
            var minmax = MakeSomeData(10);

            var ciRep2 = new CharityInfoRepository();
            var tasks = new MatchTasks(ciRep2);

            var randomCi = tasks.FetchUntilNot(minmax.Item1, minmax.Item1 - 1, minmax.Item2 + 1);

            Assert.IsNotNull(randomCi);
            Assert.AreNotEqual(minmax.Item1, randomCi.Id);
        }

        [Test]
        public void FetchUntilNot_AvoidNot()
        {
            var minmax = MakeSomeData(2);

            var ciRep2 = new CharityInfoRepository();
            var tasks = new MatchTasks(ciRep2);

            var randomCi = tasks.FetchUntilNot(minmax.Item1, minmax.Item1, minmax.Item2);

            Assert.IsNotNull(randomCi);
            Assert.AreNotEqual(minmax.Item1, randomCi.Id);
        }

        [Test]
        public void GetRandomPair_CanGet()
        {
            var minmax = MakeSomeData(10);

            var ciRep2 = new CharityInfoRepository();
            var tasks = new MatchTasks(ciRep2);

            var randomPair = tasks.GetRandomPair();

            Assert.AreEqual(2, randomPair.Count());
            Assert.AreNotEqual(randomPair[0].Id, randomPair[1].Id);



        }

        [Test]
        public void GetMaxMinId_CanGet()
        {
            var minmax = MakeSomeData(10);

            var ciRep = new CharityInfoRepository();
            var minmaxRep = ciRep.GetMaxMinId();

            Assert.AreEqual(minmax.Item1, minmaxRep.Item1);
            Assert.AreEqual(minmax.Item2, minmaxRep.Item2);


        }


    }
}
