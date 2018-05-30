using System.Collections.Generic;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Сущность зал
    /// </summary>
   public class Gym
    {
        public Gym()
        {
            ArrTrainings = new HashSet<UpcomingTraining>();
        }
        /// <summary>
        /// Id зала
        /// </summary>
        public int GymId { get; set; }

        /// <summary>
        /// Название зала
        /// </summary>
        public string GymName { get; set; }

        /// <summary>
        /// Вместимость зала
        /// </summary>
        public int GymCapacity { get; set; }

        #region Связи между сущностями 0...* Training to 1 Gym

        public virtual ICollection<UpcomingTraining> ArrTrainings { get; set; }

        #endregion
    }
}
