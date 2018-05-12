using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnesClubCL.CF_EMDB.Repositories
{
    /// <summary>
    /// Этот класс не допилен
    /// </summary>
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DataBaseFcContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить аккаунт по номеру абонемента
        /// </summary>
        /// <param name="nNumberSubscription">Номер абонемента</param>
        /// <returns>Аккаунт</returns>
        public Account GetAccountForNumberSubscription(int nNumberSubscription)
        {
            var queryGetAccountForNumberSubscription = from account in DataBaseFcContext.Accounts
                where account.NumberSubscription == nNumberSubscription
                select account;

            return queryGetAccountForNumberSubscription.SingleOrDefault();
        }

        /// <summary>
        /// Получить аккаунты, записанные на конкретную тренировку
        /// </summary>
        /// <param name="nTrainingId">Id тренировки</param>
        /// <returns> Коллекция аккаунтов, записанных на конкретную тренировку</returns>
        public IEnumerable<Account> GetAccountsWritingOnTraining(int nTrainingId)
        {
            var queryGetAccountsWritingOnTraining = from training in DataBaseFcContext.Trainings
                where training.TrainingId == nTrainingId
                select training.ArrAccounts.FirstOrDefault();
            return queryGetAccountsWritingOnTraining;
        }

        /// <summary>
        /// Получить аккаунты с истекшими абонементами
        /// </summary>
        /// <returns>Коллекция аккаунтов с истекшими абонементами</returns>
        public IEnumerable<Account> GetAccountsWichHasExpiredSubscription()
        {
            var queryGetAccountsWichHasExpiredSubscription = from account in DataBaseFcContext.Accounts
                where account.TrainingCount == 0
                select account;
            return queryGetAccountsWichHasExpiredSubscription;
        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
