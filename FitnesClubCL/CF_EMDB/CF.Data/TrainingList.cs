using System.Collections.Generic;

namespace FitnesClubCL.CF_EMDB
{

    /// <summary>
    /// Сущность  список тренировок
    /// </summary>
    public class TrainingList
    {
        public TrainingList()
        {
            ArrTrainings = new HashSet<Training>();
        }
        /// <summary>
        /// Id списка тренировок
        /// </summary>
        public int TrainingListId { get; set; }

        /// <summary>
        /// Имя тренировки
        /// </summary>
        public string TrainingListName { get; set; }

        /// <summary>
        /// Стоимость тренировки
        /// </summary>
        public double TrainingCurrentCost { get; set; }

        #region Связи между сущностями 0...* Training to 1 TrainingList

        public virtual ICollection<Training> ArrTrainings { get; set; }

        #endregion
    }
}
