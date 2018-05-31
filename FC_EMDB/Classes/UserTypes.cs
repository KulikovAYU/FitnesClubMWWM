using System;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.Classes
{

    /// <summary>
    /// "Авторизационные данные пользователя"
    /// </summary>
    public class AutorizationUserData
    {
        public AutorizationUserData()
        {

        }
        /// <summary>
        /// Логин
        /// </summary>
        public string UserLoginName { get; set; }
        /// <summary>
        /// строка пароля
        /// </summary>
        public string PasswordString { get; set; }
        /// <summary>
        /// сгенерированный хэш пароля
        /// </summary>
        public string PasswordHash { get;  private set; }

        /// <summary>
        /// Установить хэш пароля
        /// </summary>
        /// <param name="pwdHash"></param>
        public void SetPasswordHash(string pwdHash)
        {
            if (!string.IsNullOrEmpty(PasswordString))
            {
                PasswordHash = pwdHash;
            }
        }
    }

    /// <summary>
    /// Класс описывает основные данные для любого человека
    /// </summary>
    public class PersonData
    {
        private string personPhoto;
        private DateTime? _personDateOfBirdth;

        public PersonData()
        {

        }

        private PersonData(string personFirstName, string personLastName,
            string personFamilyName, DateTime? personDateOfBirdth, string personGender, int personId = 0)
        {
            PersonFirstName = personFirstName;
            PersonLastName = personLastName;
            PersonFamilyName = personFamilyName;
            PersonDateOfBirdth = personDateOfBirdth;
            PersonGender = personGender;
            PersonId = personId;
        }

        public PersonData(string personFirstName, string personLastName,
            string personFamilyName, DateTime? personDateOfBirdth, string personGender, int personId, string personAdress, string personPhoneNumber, string personMail, byte[] personPhoto, bool isExistPerson) :
            this(personFirstName, personLastName, personFamilyName, personDateOfBirdth, personGender, personId)
        {
            PersonAdress = personAdress;
            PersonPhoneNumber = personPhoneNumber;
            PersonMail = personMail;
            PersonPhoto = personPhoto;
            IsExistPerson = isExistPerson;
        }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string PersonFirstName { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string PersonFamilyName { get; set; }

        /// <summary>
        /// Отчетство клиента
        /// </summary>
        public string PersonLastName { get; set; }

        /// <summary>
        /// Роль человека
        /// </summary>
        public string PersonRole { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? PersonDateOfBirdth
        {
            get => DateTime.Parse(_personDateOfBirdth?.ToString("dd.MM.yyyy")); set => _personDateOfBirdth = value; }

        /// <summary>
        /// Id человека
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Фото человека
        /// </summary>
        public byte[] PersonPhoto { get; set; }

        /// <summary>
        /// Путь к фотографии человека
        /// </summary>
        public string PathPersonPhoto { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PersonPhoneNumber { get; set; }

        /// <summary>
        /// Мейл
        /// </summary>
        public string PersonMail { get; set; }

        /// <summary>
        /// Адрес человека
        /// </summary>
        public string PersonAdress { get; set; }

        /// <summary>
        /// Признак существования человека(записи) в БД 
        /// </summary>
        public bool IsExistPerson { get; protected set; }
        /// <summary>
        /// Пол человека
        /// </summary>
        public string PersonGender { get; set; }
    }

    /// <summary>
    /// Класс описывает данные пользователя информационной системы
    /// </summary>
    public class UserData : PersonData
    {
        public UserData( string employeeFirstName, string employeeLastName, string employeeFamilyName, DateTime? employeeDateOfBirdth, string personGender, int employeeId,
            string employeeAdress, string employeePhoneNumber, string employeeMail, byte[] employeePhoto, string employeeLoginName, string employeePasswordHash, 
            EmployeeRole employeeRole, string employeeWorkingStatus, bool isExistPerson = false) : 
            base( employeeFirstName, employeeLastName, employeeFamilyName, employeeDateOfBirdth, personGender, employeeId, employeeAdress, employeePhoneNumber, employeeMail, employeePhoto, isExistPerson)
        {
            EmployeePasswordHash = employeePasswordHash;
            EmployeeLoginName = employeeLoginName;
            EmployeWorkingStatus = employeeWorkingStatus;
            EmployeRole = employeeRole;
          //  base.SavePhoto();
        }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string EmployeePasswordHash { get; private set; }

        /// <summary>
        /// логин
        /// </summary>
        public string EmployeeLoginName { get; private set; }

        /// <summary>
        /// статус работника
        /// </summary>
        public string EmployeWorkingStatus { get; private set; }
        
        /// <summary>
        /// Роль работника
        /// </summary>
        public EmployeeRole EmployeRole { get; private set; }
      
        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string EmployeeFullName => PersonFirstName+" " + PersonLastName + " " + PersonFamilyName;
    }

    /// <summary>
    /// Класс описывает данные клиента
    /// </summary>
    public class NewClientData : PersonData
    {

        public NewClientData(bool bIsnewClient = false) : base()
        {
            IsExistPerson = bIsnewClient;

            if (bIsnewClient) // в случае если это новый клиент, добавляем время регистрации
            {
                AccountregistrationDate = DateTime.Now;
            }
        }
        /// <summary>
        /// Паспортные данные клиента
        /// </summary>
        /// Серия
        public string ClientPasportDataSeries { get;set; }
        /// <summary>
        /// Номер
        /// </summary>
        public string ClientPasportDataNumber { get; set; }
        /// <summary>
        /// Кем выдан
        /// </summary>
        public string ClientPasportDataIssuedBy { get; set; }
        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? ClientPasportDatеOfIssue { get;  set; }
        /// <summary>
        /// Номер абонемента
        /// </summary>
        public int NumberSubscription { get;  set; }
        /// <summary>
        /// Когда зарегистрирован абонемент
        /// </summary>
        public DateTime? AccountregistrationDate { get; private set; } 
    }
}
