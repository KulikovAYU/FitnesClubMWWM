using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Статус аккаунта (активен, заморожен)
    /// </summary>
    public class AccountStatus : ViewModelBase
    {
        public AccountStatus()
        {
            ArrAccounts = new HashSet<Account>();
        }

        /// <summary>
        /// Id статуса аакаунта
        /// </summary>
        public int AccountStatusId { get; set; }

        /// <summary>
        /// Название статуса
        /// </summary>
        public string AccountStatusName { get; set; }

        #region Связи между сущностями 0...* Account to 1 AccountStatus

        public virtual ICollection<Account> ArrAccounts { get; set; }

        #endregion

    }
}
