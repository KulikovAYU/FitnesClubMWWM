using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.Data.Repositories
{
   public interface IGymRepository : IRepository<Gym>
    {
        void CreateOrUpdateRecord(Gym data);
    }
}
