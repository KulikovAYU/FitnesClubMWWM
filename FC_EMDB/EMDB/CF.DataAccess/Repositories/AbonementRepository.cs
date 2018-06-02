using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
    class AbonementRepository :  Repository<Abonement>, IAbonementRepository
    {
        public AbonementRepository(DbContext context) : base(context)
        {
        }

        public Account GetAccountForNumberSubscription(int nNumberSubscription)
        {
            return  DataBaseFcContext.Abonements.FirstOrDefault(number => number.NumberSubscription == nNumberSubscription)?.Account;
        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
