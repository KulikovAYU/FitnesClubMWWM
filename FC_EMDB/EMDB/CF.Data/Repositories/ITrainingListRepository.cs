using System.Collections.ObjectModel;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.Data.Repositories
{
  public  interface ITrainingListRepository : IRepository<PriceTrainingList>
    {
        /// <summary>
        /// Получить доступные для клиента тренировки
        /// </summary>
        /// <param name="servicesInSubscription">тренировки пользователя</param>
        ObservableCollection<PriceTrainingList> GetAvailableTrainings(ServicesInSubscription servicesInSubscription);
    }
}
