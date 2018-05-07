using System;
using System.Collections.Generic;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Сущность работника
    /// </summary>
   public class Employee
    {
        public Employee()
        {
            ArrAccounts = new HashSet<Account>();
            ArrTrainings = new HashSet<Training>();
        }

        /// <summary>
        /// Id клиента
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// имя клиента
        /// </summary>
        public string EmployeeFirstName { get; set; }

        /// <summary>
        /// Отчетство клиента
        /// </summary>
        public string EmployeeLastName { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string EmployeeFamilyName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? EmployeeDateOfBirdth { get; set; }

        /// <summary>
        /// Адрес клиента
        /// </summary>
        public string EmployeeAdress { get; set; }

        /// <summary>
        /// Номер телефона клиента
        /// </summary>
        public string EmployeePhoneNumber { get; set; }

        /// <summary>
        /// е-мэйл клиента
        /// </summary>
        public string EmployeeMail { get; set; }

        /// <summary>
        /// Фото клиента
        /// </summary>
        public byte[] EmployeePhoto { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string EmployeeLoginName { get; set; }

        /// <summary>
        /// Пароль (хэш пароля)
        /// </summary>
        public string EmployeePasswordHash { get; set; }

        /// <summary>
        /// Статус работника (в отпуске или нет)
        /// </summary>
        public string EmployeeVacationStatus { get; set; }

        #region Связи между сущностями 1 Employee to 0...* Account

        /// <summary>
        /// Свойство навигации (Virtual ->Lazy Load)
        /// </summary>
        public virtual ICollection<Account> ArrAccounts { get; set; }
        #endregion


        #region Связи между сущностями 0..1 Employee to 0...* Training

        /// <summary>
        /// Свойство навигации (Virtual ->Lazy Load)
        /// </summary>
        public virtual ICollection<Training> ArrTrainings { get; set; }
        #endregion

        #region Связи между сущностями 1..1 Salary to 0...* Employee

        /// <summary>
        /// Свойство навигации (Virtual ->Lazy Load)
        /// </summary>
        public virtual Salary Salary { get; set; }
        #endregion


        #region Связи между сущностями 1..1 EmployeeRole to 0...* Employee

        /// <summary>
        /// Свойство навигации (Virtual ->Lazy Load)
        /// </summary>
        public virtual EmployeeRole EmployeeRole { get; set; }
        #endregion

    }
}
