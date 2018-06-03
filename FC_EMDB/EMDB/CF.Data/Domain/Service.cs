using System.Collections.Generic;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Сущность "услуга" (тренировка)
    /// </summary>
    public class Service
    {
        public Service()
        {
            Trainings = new HashSet<UpcomingTraining>();
        }
        /// <summary>
        /// Id услуги
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Название услуги
        /// </summary>
        public string ServiceName { get; set; }

      
        public virtual ICollection<UpcomingTraining> Trainings { get; set; }


      
    }
}
