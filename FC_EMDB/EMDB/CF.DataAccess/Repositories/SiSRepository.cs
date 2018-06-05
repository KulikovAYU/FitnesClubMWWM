using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
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
            if(service == null)
                return;
            DataBaseFcContext.ServicesInSubscription.AddOrUpdate(service);
            DataBaseFcContext.SaveChanges();
        }

        public void IncrementCountServices(UpcomingTraining upcTraining)
        {
            if(upcTraining == null)
                return;
           var query = DataBaseFcContext.ServicesInSubscription.FirstOrDefault(upcTrain => upcTrain.PriceType.TrainingListName.ServiceName == upcTraining.Service.ServiceName);
            if (query == null)
            {
                return;
            }
            query.SiSTrainingCount++;
            
            DataBaseFcContext.SaveChanges();
        }

        /// <summary>
        /// Посчитать общую стоимость абонемента
        /// </summary>
        /// <param name="accountAbonement">Текущий абонемент</param>
        public void SetTotalCost(Abonement accountAbonement)
        {
            if (accountAbonement == null)
                return;
            var query = DataBaseFcContext.Abonements.FirstOrDefault(ab=>ab.NumberSubscription == accountAbonement.NumberSubscription);

            if (query != null)
            {
                query.AbonementTotalCost = 0;
                foreach (var buyingTraining in query.ArrServicesInSubscription)
                {
                    buyingTraining.TotalCost =
                        buyingTraining.SiSTrainingCount * buyingTraining.PriceType.TrainingCurrentCost;

                    query.AbonementTotalCost += Math.Round(buyingTraining.TotalCost, 2);
                }
            }

            DataBaseFcContext.SaveChanges();
        }
    }
}
