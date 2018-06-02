using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.Data.Repositories
{
    public interface IAbonementRepository : IRepository<Abonement>
    {
        /// <summary>
        /// Получить аккаунт по номеру абонемента
        /// </summary>
        /// <param name="nNumberSubscription">Номер абонемента</param>
        /// <returns>Аккаунт</returns>
        Account GetAccountForNumberSubscription(int nNumberSubscription);
    }
}
