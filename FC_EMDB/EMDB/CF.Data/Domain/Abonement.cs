using System;
using System.Collections.Generic;
using FC_EMDB.Utils;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Сущность абонемента
    /// </summary>
    public class Abonement
    {
        public Abonement()
        {
            ArrServicesInSubscription = new HashSet<ServicesInSubscription>();
            ArrUpcomingTrainings = new HashSet<UpcomingTraining>();
        }

        public int AbonementId { get; set; }

        public int NumberSubscription { get; private set; }= AbonementGenerator.CreateNumberSubscription();

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime? AbonementregistrationDate { get; private set; } = DateTime.Now;


        /// <summary>
        ///Общее Количество тренировок 
        /// </summary>
        public int TrainingCount { get; set; }


        /// <summary>
        /// Количество посещенных тренировок
        /// </summary>
        public int VisitedTrainingCount { get; set; }

        /// <summary>
        /// Общая стоимость абонемента
        /// </summary>
        public Decimal TotalCost { get; set; }


        ///// <summary>
        ///// Тип Абонемента
        ///// </summary>
        //public string StrTypeAbonement => TypeAbonement?.TrainingListName.ServiceName;


        /// <summary>
        /// Срок действия абонемена (дни)
        /// </summary>
        public DateTime? CountDays { get; set; }

        /// <summary>
        /// Дата активации абонемента
        /// </summary>
        public DateTime? AbonementActivationDateTime { get; set; } = DateTime.Now;

        #region Связи между сущностями 0...* Account to 0...* Training
        /// <summary>
        /// Количество купленных занятий
        /// </summary>
        public virtual ICollection<ServicesInSubscription> ArrServicesInSubscription { get; set; }

        #endregion

        public virtual Account Account { get; set; }

        public virtual AbonementStatus AbonmentStatus { get; set; }

        public virtual ICollection<UpcomingTraining> ArrUpcomingTrainings { get; set; }

    }
}
