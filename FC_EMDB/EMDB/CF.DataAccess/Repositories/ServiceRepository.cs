using System.Collections.ObjectModel;
using System.Linq;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
    class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(DataBaseFcContext context) : base(context)
        {

        }

      
        public ObservableCollection<UpcomingTraining> GetAvailableTrainings(ServicesInSubscription servicesInSubscription)
        {
          var query =  DataBaseFcContext.UpcomingTrainings.Where(upc =>
                upc.Service.ServiceName == servicesInSubscription.PriceType.TrainingListName.ServiceName);

            return new ObservableCollection<UpcomingTraining>(query.Cast<UpcomingTraining>());
        }

        public bool CheckTrainingOnAvailable(UpcomingTraining item)
        {
            if (item == null) return false;
            var query = DataBaseFcContext.UpcomingTrainings.FirstOrDefault(upc => upc.TrainingId == item.TrainingId);
            return    query != null && query.NumberOfSeats != 0;
        }


        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
