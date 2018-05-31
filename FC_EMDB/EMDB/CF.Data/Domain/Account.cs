using System;
using System.Collections.Generic;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    //Сущность аккаунта посетителя
    public class Account : Human
    {
        public Account()
        {
            //ArrTrainings = new HashSet<UpcomingTraining>();
            //ArrTrainingsList =new HashSet<TrainingList>();
            //ArrServicesInSubscription = new HashSet<ServicesInSubscription>();
        }

        //#region Описание самого аккаунта (абонемента)
        ///// <summary>
        ///// Номер абонемента
        ///// </summary>
        //public int NumberSubscription { get; set; }

        ///// <summary>
        ///// Дата регистрации
        ///// </summary>
        //public DateTime? AccountregistrationDate { get; set; }

        ///// <summary>
        /////Количество тренировок 
        ///// </summary>
        //public int TrainingCount { get; set; }

        ///// <summary>
        ///// Количество посещенных тренировок
        ///// </summary>
        //public int VisitedTrainingCount { get; set; }

        ///// <summary>
        ///// Общая стоимость
        ///// </summary>
        //public Decimal TotalCost { get; set; } = 100;

        ///// <summary>
        ///// Тип Абонемента
        ///// </summary>
        //public string StrTypeAbonement => TypeAbonement?.TrainingListName.ServiceName;

        ///// <summary>
        ///// Срок действия абонемена (дни)
        ///// </summary>
        //public int CountDays { get; set; }

        ///// <summary>
        ///// Дата активации абонемента
        ///// </summary>
        //public DateTime? AbonementActivationDateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Полное ФИО (в сохранении бд не участвует)
        /// </summary>
        public string StrFullName => $"{HumanFamilyName} {HumanFirstName} {HumanLastName}";



        //#endregion

        //#region Описание личных данных клиента
        ///// <summary>
        ///// Id клиента (ключ PK)
        ///// </summary>
        //public int ClientId { get; set; }

        ///// <summary>
        ///// Имя клиента
        ///// </summary>
        //public string ClientFirstName { get; set; }

        ///// <summary>
        ///// Отчетство клиента
        ///// </summary>
        //public string ClientLastName { get; set; }

        ///// <summary>
        ///// Фамилия клиента
        ///// </summary>
        //public string ClientFamilyName { get; set; }

        ///// <summary>
        ///// Дата рождения клиента
        ///// </summary>
        //public DateTime? ClientDateOfBirdth { get; set; }

        ///// <summary>
        ///// Пол клиента
        ///// </summary>
        //public string ClientGender { get; set; }

        ///// <summary>
        ///// Адрес клиента
        ///// </summary>
        //public string ClientAdress { get; set; }

        ///// <summary>
        ///// Номер телефона клиента
        ///// </summary>
        //public string ClientPhoneNumber { get; set; }

        ///// <summary>
        ///// е-мэйл клиента
        ///// </summary>
        //public string ClientMail { get; set; }

        ///// <summary>
        ///// Фото клиента
        ///// </summary>
        //public byte[] ClientPhoto { get; set; }

        ///// <summary>
        ///// Путь к фотографии (в сохранении БД не участвует)
        ///// </summary>
        //public string StrPathPhoto { get; set; }



        ///// <summary>
        ///// Паспортные данные клиента
        ///// </summary>
        ///// Серия
        //public string ClientPasportDataSeries { get; set; }
        ///// <summary>
        ///// Номер
        ///// </summary>
        //public string ClientPasportDataNumber { get; set; }
        ///// <summary>
        ///// Кем выдан
        ///// </summary>
        //public string ClientPasportDataIssuedBy { get; set; }
        ///// <summary>
        ///// Дата выдачи
        ///// </summary>
        //public DateTime? ClientPasportDatеOfIssue { get; set; }
        //#endregion

        public virtual Abonement Abonement { get; set; }

      

        //#region Связи между сущностями 1...1 Account to 1...* TrainingList
        //public virtual PriceTrainingList TypeAbonement { get; set; }
        //#endregion


        //#region Связи между сущностями 0...* Account to 0...* Training

        //public virtual ICollection<UpcomingTraining> ArrTrainings { get; set; }

        //#endregion

        #region Связи между сущностями 0...* Account to 1 AccountStatus

         public virtual AccountStatus AccountStatus { get; set; }

        #endregion

        //#region Связи между сущностями 0...* Account to 0...* Training
        ///// <summary>
        ///// Количество купленных занятий
        ///// </summary>
        //public virtual ICollection<ServicesInSubscription> ArrServicesInSubscription { get; set; }

        //#endregion

      //  public ObservableCollection<ServicesInSubscription> OcAccounts  => new ObservableCollection<ServicesInSubscription>(ArrServicesInSubscription.Cast<ServicesInSubscription>());

     
    }
}
