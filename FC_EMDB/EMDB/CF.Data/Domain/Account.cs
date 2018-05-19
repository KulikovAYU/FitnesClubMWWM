using System;
using System.Collections.Generic;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    //Сущность аккаунта посетителя
    public class Account
    {
        public Account()
        {
            ArrTrainings = new HashSet<Training>();
            ArrTrainingsList =new HashSet<TrainingList>();
        }
        #region Описание самого аккаунта (абонемента)
        /// <summary>
        /// Номер абонемента
        /// </summary>
        public int NumberSubscription { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime AccountregistrationDate { get; set; }

        /// <summary>
        ///Количество тренировок 
        /// </summary>
        public int TrainingCount { get; set; }

        /// <summary>
        /// Количество посещенных тренировок
        /// </summary>
        public int VisitedTrainingCount { get; set; }
        
        /// <summary>
        /// Общая стоимость
        /// </summary>
        public Decimal TotalCost { get; set; }
        #endregion

        #region Описание личных данных клиента
        /// <summary>
        /// Id клиента (ключ PK)
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string ClientFirstName { get; set; }

        /// <summary>
        /// Отчетство клиента
        /// </summary>
        public string ClientLastName { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string ClientFamilyName { get; set; }

        /// <summary>
        /// Дата рождения клиента
        /// </summary>
        public DateTime? ClientDateOfBirdth { get; set; }

        /// <summary>
        /// Пол клиента
        /// </summary>
        public string ClientGender { get; set; }

        /// <summary>
        /// Адрес клиента
        /// </summary>
        public string ClientAdress { get; set; }

        /// <summary>
        /// Номер телефона клиента
        /// </summary>
        public string ClientPhoneNumber { get; set; }

        /// <summary>
        /// е-мэйл клиента
        /// </summary>
        public string ClientMail { get; set; }

        /// <summary>
        /// Фото клиента
        /// </summary>
        public byte[] ClientPhoto { get; set; }

        /// <summary>
        /// Паспортные данные клиента
        /// </summary>
        /// Серия
        public string ClientPasportDataSeries { get; set; }
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
        public DateTime ClientPasportDatеOfIssue { get; set; }
        #endregion

        #region Связи между сущностями 1 Employee to 0...* Account
        public Employee Employee { get; set; }
        #endregion

        #region Связи между сущностями 1...* Account to 1...* TrainingList
        public ICollection<TrainingList> ArrTrainingsList { get; set; }
        #endregion


        #region Связи между сущностями 0...* Account to 0...* Training

        public ICollection<Training> ArrTrainings { get; set; }

        #endregion

        #region Связи между сущностями 0...* Account to 1 AccountStatus

         public AccountStatus AccountStatus { get; set; }

        #endregion
    }
}
