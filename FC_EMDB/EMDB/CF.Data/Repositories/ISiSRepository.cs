using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.Data.Repositories
{
    public  interface ISiSRepository : IRepository<ServicesInSubscription>
  {
      void AddOrUpdate(ServicesInSubscription service);
      void IncrementCountServices(UpcomingTraining upcTraining);
  }
}
