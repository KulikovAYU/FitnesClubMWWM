using System.Collections.Generic;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Статус аккаунта (активен, заморожен)
    /// </summary>
    public class AccountStatus
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

        public ICollection<Account> ArrAccounts { get; set; }

        #endregion

    }
}
