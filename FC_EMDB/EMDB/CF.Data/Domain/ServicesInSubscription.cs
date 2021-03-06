﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GalaSoft.MvvmLight;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Сущность "услуги в абонементе" (тренировки и др..)
    /// </summary>
   public class ServicesInSubscription : ViewModelBase
    {
        public ServicesInSubscription()
        {
            ArrAbonements = new HashSet<Abonement>();
        }

        public ServicesInSubscription(ServicesInSubscription service)
        {
            this.SiSId = service.SiSId;
            this.SiSTrainingCount = service.SiSTrainingCount;
            this.SiSVisitedTrainingCount = service.SiSVisitedTrainingCount;
            this.SiSTrainingName = service.SiSTrainingName;
            this.TotalCost = service.TotalCost;
            this.PriceType = service.PriceType;
            this.ArrAbonements = service.ArrAbonements;
        }

        /// <summary>
        /// Id
        /// </summary>
        public int SiSId { get; set; }


        /// <summary>
        /// Тип Абонемента
        /// </summary>
        // public string SiSType => arrPriceType?.TrainingListName.ServiceName;
        // public virtual PriceTrainingList SiSType { get; set; }

        /// <summary>
        ///Количество тренировок 
        /// </summary>
        public int SiSTrainingCount { get; set; }


      
        /// <summary>
        ///Количество посещенных тренировок 
        /// </summary>
        public int SiSVisitedTrainingCount { get; set; }



        public string SiSTrainingName { get; set; }

        /// <summary>
        /// Общая стоимость за конкретное занятие
        /// </summary>
        public Decimal TotalCost { get; set; }


        #region Связи между сущностями 1...1 Account to 1...* TrainingList
        public virtual PriceTrainingList PriceType { get; set; } //Вид услуги, Тариф
        #endregion

        //public virtual ICollection<Account> arrAccount { get; set; }
        public virtual ICollection<Abonement> ArrAbonements { get; set; }

    }
}
