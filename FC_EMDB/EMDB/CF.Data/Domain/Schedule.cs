using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Расписание занятий
    /// </summary>
   public class Schedule : ViewModelBase
    {
        public Schedule()
        {
            PriceTrainingList = new HashSet<PriceTrainingList>();
        }

        /// <summary>
        /// Время начала
        /// </summary>
        public DateTime StartDateTime { get; set; }

        public virtual ICollection<PriceTrainingList> PriceTrainingList { get; set; }



        public virtual ICollection<Account> Accounts { get; set; }

    }
}
