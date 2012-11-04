using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wwwtw.Domain;
using SharpArch.NHibernate.Contracts.Repositories;
using Wwwtw.Infrastructure;

namespace Wwwtw.Tasks
{
    public class MatchTasks
    {
        private readonly CharityInfoRepository charityInfoRepository;
        private static Random rand = new Random();
        private static Object m_lock = new Object();
        private static Tuple<int, int> m_minmax = null;



        public MatchTasks(CharityInfoRepository cir)
        {
            charityInfoRepository = cir;
        }


        public void GetMinMax()
        {
            if (m_minmax == null)
            {
                lock(m_lock)
                {
                    if (m_minmax == null)
                        m_minmax = charityInfoRepository.GetMaxMinId();
                }
            }
        }

        // for testing
        public static void ClearMinMax()
        {
            m_minmax = null;
        }

        public List<CharityInfo> GetRandomPair()
        {
            GetMinMax();
            List<CharityInfo> ret = new List<CharityInfo>();
            var first = FetchUntilNot(-1, m_minmax.Item1, m_minmax.Item2);
            int avoidId = first.Id;
            ret.Add(first);
            ret.Add(FetchUntilNot(avoidId, m_minmax.Item1, m_minmax.Item2));
            return ret;
        }

        public void MatchResult(int winnerId, int loserId)
        {
            var winner = charityInfoRepository.Get(winnerId);
            var loser = charityInfoRepository.Get(loserId);

            var elo = new EloRating(winner.GameScore, loser.GameScore, 400, 0);

            winner.GameScore = Convert.ToInt32( Math.Round(elo.FinalResult1));
            loser.GameScore = Convert.ToInt32(Math.Round(elo.FinalResult2));

            charityInfoRepository.SaveOrUpdate(winner);
            charityInfoRepository.SaveOrUpdate(loser);
            charityInfoRepository.DbContext.CommitChanges();


        }

        public CharityInfo FetchUntilNot(int notId, int minId, int maxId)
        {
            int tries = 0;
            CharityInfo ret = null;
            while (ret == null && tries < 40)
            {
                int randomId = rand.Next(minId, maxId+1);
                if (randomId != notId)
                {
                    ret = charityInfoRepository.Get(randomId);
                }
                tries += 1;
            }
            if (ret == null)
                throw new ApplicationException(string.Format("FetchUntilNot failed to find a random entity after {0} attempts with min {1} max {2}", tries, minId, maxId));

            return ret;
        }
    }
}
