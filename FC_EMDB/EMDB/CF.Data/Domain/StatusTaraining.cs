using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    public class StatusTraining : ViewModelBase
    {
        public StatusTraining()
        {
            Trainings = new HashSet<UpcomingTraining>();
        }
        /// <summary>
        /// Id статуса тренировки
        /// </summary>
        public int StatusTarainingId { get; set; }

        /// <summary>
        /// Описание статуса тренировки
        /// </summary>
        public string StatusName { get; set; }


        public ICollection<UpcomingTraining> Trainings { get; set; }
    }
}
