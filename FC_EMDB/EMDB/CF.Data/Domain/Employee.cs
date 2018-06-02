using System.Collections.Generic;


namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Сущность работника
    /// </summary>
    public class Employee : Human
    {
        public Employee()
        {
            ArrAccounts = new HashSet<Account>();
            ArrTrainings = new HashSet<UpcomingTraining>();
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string EmployeeLoginName { get; set; }

        /// <summary>
        /// Пароль (хэш пароля)
        /// </summary>
        public string EmployeePasswordHash { get; set; }

      
        #region Связи между сущностями 1 Employee to 0...* Account
        /// <summary>
        /// Свойство навигации (Virtual ->Lazy Load)
        /// </summary>
        public virtual ICollection<Account> ArrAccounts { get; set; }

        #endregion

        #region Связи между сущностями 0...* Employee to 1...1 EmployeeWorkingStatus

        /// <summary>
        /// Статус работника (в отпуске или нет)
        /// </summary>
        public virtual EmployeeWorkingStatus EmployeeWorkingStatus { get; set; }

        #endregion

        public virtual EmployeeRole EmployeeRole { get; set; }
        #region Связи между сущностями 0..1 Employee to 0...* Training

        /// <summary>
        /// Свойство навигации (Virtual ->Lazy Load)
        /// </summary>
         public virtual ICollection<UpcomingTraining> ArrTrainings { get; set; } //Отладка
        #endregion

    }
}
