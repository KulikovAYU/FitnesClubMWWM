using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
    class AccountStatusRepository : Repository<AccountStatus>, IAccountStatusRepository
    {
        public AccountStatusRepository(DataBaseFcContext context) : base(context)
        {

        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
