using System.Data.Entity;
using System.Data.Entity.Migrations;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
    class SiSRepository : Repository<ServicesInSubscription>, ISiSRepository
    {
        public SiSRepository(DbContext context) : base(context)
        {
        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;

        public void AddOrUpdate(ServicesInSubscription service)
        {
            DataBaseFcContext.ServicesInSubscription.AddOrUpdate(service);
            DataBaseFcContext.SaveChanges();
        }
    }
}
