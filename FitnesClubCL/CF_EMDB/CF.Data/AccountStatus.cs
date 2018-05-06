namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Статус аккаунта (активен, заморожен)
    /// </summary>
    class AccountStatus
    {
        /// <summary>
        /// Id статуса аакаунта
        /// </summary>
        public int AccountStatusId { get; set; }

        /// <summary>
        /// Название статуса
        /// </summary>
        public string AccountStatusName { get; set; }

    }
}
