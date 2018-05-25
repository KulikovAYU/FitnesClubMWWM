using System.Collections.Generic;
using System.Drawing;
using FC_EMDB.Classes;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.Interfaces;


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
        /// Получить список акаунтов с истекшим абонементом
        /// </summary>
        /// <returns></returns>
        IEnumerable<Account> GetAccountsWichHasExpiredSubscription();

        /// <summary>
        /// Найти аккаунт с определенными данными
        /// </summary>
        /// <param name="clientData"> Данные по которым будет производиться выборка</param>
        /// <returns>Аккаунт</returns>
        NewClientData FindAccountWithSameData(NewClientData clientData);

        /// <summary>
        /// Обновить поля (запись) клиента в БД
        /// </summary>
        /// <param name="clientData">Данные клиента</param>
        void UpdateFields(NewClientData clientData);
    }
}
