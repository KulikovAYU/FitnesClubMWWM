using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.Data.Repositories
{
  public  interface IUpcomingTrainingRepository : IRepository<UpcomingTraining>
  {
      void AddNewUpcomingTraining(Account account, UpcomingTraining newTraining);
      void FixTheVisit(Account account, UpcomingTraining upcomingTraining);
      UpcomingTraining GetUpcomingTraining( UpcomingTraining upcTraining);
  }
}
