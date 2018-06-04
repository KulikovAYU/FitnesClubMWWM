using System.Data.Entity;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
   public class PriceTrainingListRepository : Repository<PriceTrainingList>, IpriceTrainingListRepository
    {
        public PriceTrainingListRepository(DbContext context) : base(context)
        {

        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
