using NHibernate.Criterion;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wwwtw.Domain;

namespace Wwwtw.Infrastructure
{
    public class CharityInfoRepository : NHibernateRepository<CharityInfo>
    {

        public virtual Tuple<int, int> GetMaxMinId()
        {
            int max = (int)Session.CreateCriteria<CharityInfo>()
                .SetProjection(Projections.Max("Id"))
                .UniqueResult();
            int min = (int)Session.CreateCriteria<CharityInfo>()
                .SetProjection(Projections.Min("Id"))
                .UniqueResult();
            return new Tuple<int, int>(min, max);
        }

        public virtual IList<CharityInfo> GetTop(int howmany)
        {
            var coll = Session.CreateCriteria<CharityInfo>().AddOrder(new Order("GameScore", false))
                .SetFirstResult(0).SetMaxResults(howmany);
            return coll.List<CharityInfo>();
        }

    }
}
