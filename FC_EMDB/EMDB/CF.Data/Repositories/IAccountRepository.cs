using System.Collections.Generic;
using FC_EMDB.EMDB.CF.Data.Domain;


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
        Account FindAccountWithSameData(Account clientData);

        /// <summary>
        /// Обновить поля (запись) клиента в БД
        /// </summary>
        /// <param name="clientData">Данные клиента</param>
        void UpdateFields(Account clientData);

        /// <summary>
        /// Создать гновую запись в БД
        /// </summary>
        /// <param name="data">данные</param>
        void CreateRecord(Account data);

        /// <summary>
        /// Добавить запись к существующему аккаунту
        /// </summary>
        /// <param name="nNumberSubscription"> Номер абонемента</param>
        void AppentRecordToExistAccount(Account nNumberSubscription);
        void AddTraining(Account account);

        //void AddServicesInSubscription(int nId, ServicesInSubscription service);
    }


}
