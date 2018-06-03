using System.Collections.ObjectModel;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.Data.Repositories
{
   public interface IServiceRepository : IRepository<Service>
    {
        ObservableCollection<UpcomingTraining> GetAvailableTrainings(ServicesInSubscription servicesInSubscription);
        bool CheckTrainingOnAvailable(UpcomingTraining item);
    }
}
