using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
    public class TarifRepository : Repository<Tarif>, ITarifRepository
    {
        public TarifRepository(DataBaseFcContext context) : base(context)
        {

        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
