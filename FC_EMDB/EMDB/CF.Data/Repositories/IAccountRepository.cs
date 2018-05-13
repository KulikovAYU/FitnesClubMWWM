using System.Collections.Generic;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnesClubCL.CF_EMDB.Repositories;


namespace FC_EMDB.EMDB.CF.Data.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {

        /// <summary>
        /// Получить аккаунт по номеру абонемента
        /// </summary>
        /// <param name="nNumberSubscription">Номер абонемента</param>
        /// <returns>Аккаунт</returns>
        Account GetAccountForNumberSubscription(int nNumberSubscription);
        /// <summary>
        /// Получить список аккаунтов, записанных на конкретную тренировку
        /// </summary>
        /// <returns></returns>
        IEnumerable<Account> GetAccountsWritingOnTraining(int nTrainingId);

        /// <summary>
        /// Получить список аакаунтов с истекшим абонементом
        /// </summary>
        /// <returns></returns>
        IEnumerable<Account> GetAccountsWichHasExpiredSubscription();

    }
}
