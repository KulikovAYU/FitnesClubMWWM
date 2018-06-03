using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Расписание занятий
    /// </summary>
   public class Schedule
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
