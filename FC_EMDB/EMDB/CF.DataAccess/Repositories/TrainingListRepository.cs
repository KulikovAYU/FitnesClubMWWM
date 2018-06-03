using System.Collections.ObjectModel;
using System.Linq;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
    public class TrainingListRepository : Repository<PriceTrainingList>, ITrainingListRepository
    {
        public TrainingListRepository(DataBaseFcContext context) : base(context)
        {
            
        }

        public ObservableCollection<PriceTrainingList> GetAvailableTrainings(ServicesInSubscription servicesInSubscription)
        {
            var priceTrainingList = DataBaseFcContext.TrainingLists.Where(trainName=> trainName.TrainingListName.ServiceName == servicesInSubscription.PriceType.TrainingListName.ServiceName);

            return new ObservableCollection<PriceTrainingList>(priceTrainingList.Cast<PriceTrainingList>());
       
        }


        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
