using System.Data.Entity;
using System.Linq;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий предстоящих тренировок
    /// </summary>
    class UpcomingTrainingRepository : Repository<UpcomingTraining>, IUpcomingTrainingRepository
    {
        public UpcomingTrainingRepository(DbContext context) : base(context)
        {

        }

        /// <summary>
        /// Добавить новую предстоящую тренировку
        /// </summary>
        public void AddNewUpcomingTraining(Account account,UpcomingTraining newTraining)
        {
            if(account == null || newTraining == null)
                return;

            //1.Нашли аккаунт
            var currentAbonement = DataBaseFcContext.Abonements.FirstOrDefault(abonem => abonem.AbonementId == account.Abonement.AbonementId);
            //2. Добавили новую тренировку
            currentAbonement?.ArrUpcomingTrainings.Add(newTraining);

            DataBaseFcContext.SaveChanges();

        }

        /// <summary>
        /// Звафиксировыать посещение тренировки
        /// </summary>
        /// <param name="account">Учетная запись</param>
        /// <param name="upcomingTraining">Предстоящая тренировка</param>
        public void FixTheVisit(Account account, UpcomingTraining upcomingTraining)
        {
            if (account == null || upcomingTraining == null)
                return;
            //1.Нашли аккаунт
            var currentAbonement = DataBaseFcContext.Abonements.FirstOrDefault(abonem => abonem.AbonementId == account.Abonement.AbonementId);
            //2. Удалили тренировку из записи
            currentAbonement?.ArrUpcomingTrainings.Remove(upcomingTraining);
            DataBaseFcContext.SaveChanges();
        }

        /// <summary>
        /// Вернуть предстоящую тренировку
        /// </summary>
        /// <param name="upcTraining"></param>
        public UpcomingTraining GetUpcomingTraining(UpcomingTraining upcTraining)
        {
            if (upcTraining == null)
                return null;

            return DataBaseFcContext.UpcomingTrainings.FirstOrDefault(upcT => upcT.TrainingId == upcTraining.TrainingId);
        }


        /// <summary>
        /// Отказ о посещении тренировки
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <param name="upcTraining">предстоящая тренировка</param>
        public void Ge(Account account, UpcomingTraining upcTraining)
        {
         
            throw new System.NotImplementedException();
        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
