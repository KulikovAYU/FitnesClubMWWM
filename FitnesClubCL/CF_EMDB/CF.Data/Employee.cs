using System;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Сущность работника
    /// </summary>
   public class Employee
    {
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
    }
}
