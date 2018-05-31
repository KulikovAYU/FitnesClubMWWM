using System;
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

        ///// <summary>
        ///// Id работника
        ///// </summary>
        //public int EmployeeId { get; set; }

        ///// <summary>
        ///// имя работника
        ///// </summary>
        //public string EmployeeFirstName { get; set; }

        ///// <summary>
        ///// Отчетство работника
        ///// </summary>
        //public string EmployeeLastName { get; set; }

        ///// <summary>
        ///// Фамилия работника
        ///// </summary>
        //public string EmployeeFamilyName { get; set; }

        ///// <summary>
        ///// Дата рождения
        ///// </summary>
        //public DateTime EmployeeDateOfBirdth { get; set; }

        ///// <summary>
        ///// Адрес работника
        ///// </summary>
        //public string EmployeeAdress { get; set; }

        ///// <summary>
        ///// Номер телефона работника
        ///// </summary>
        //public string EmployeePhoneNumber { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string EmployeeLoginName { get; set; }

        /// <summary>
        /// Пароль (хэш пароля)
        /// </summary>
        public string EmployeePasswordHash { get; set; }

        ///// <summary>
        ///// е-мэйл работника
        ///// </summary>
        //public string EmployeeMail { get; set; }

        ///// <summary>
        ///// Фото работника
        ///// </summary>
        //public byte[] EmployeePhoto { get; set; }

       
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
