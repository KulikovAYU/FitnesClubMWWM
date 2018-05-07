using System;
using System.Collections.Generic;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Описание сущности "тренировка"
    /// </summary>
    public class Training
    {
        public Training()
        {
            ArrAccounts = new HashSet<Account>();
        }
        /// <summary>
        /// Id тренировки
        /// </summary>
        public int TrainingId { get; set; }

        /// <summary>
        /// Продолжительность тренировки
        /// </summary>
        public DateTime TrainingDuration { get; set; }

        /// <summary>
        /// Дата тренировки
        /// </summary>
        public DateTime TrainingStartDate { get; set; }

        /// <summary>
        /// Время начала тренировки
        /// </summary>
        public DateTime TrainingStartTime { get; set; }

        /// <summary>
        /// Количество мест
        /// </summary>
        public int NumberOfSeats { get; set; }


        /// <summary>
        /// Статус трениорвки: Закончена или идет запись, группа набрана
        /// </summary>
        public string StatusTaraining { get; set; }

        #region Связи между сущностями 0...* Account to 0...* Training

        public virtual ICollection<Account> ArrAccounts { get; set; }

        #endregion

        #region Связи между сущностями 0...* Training to 1 TrainingList

        public virtual TrainingList TrainingList { get; set; }

        #endregion

        #region Связи между сущностями 0...* Training to 1 Gym

        public virtual Gym Gym { get; set; }

        #endregion

        #region Связи между сущностями 0..1 Employee to 0...* Training

        /// <summary>
        /// Свойство навигации (Virtual ->Lazy Load)
        /// </summary>
        public virtual Employee Employee { get; set; }
        #endregion

    }
}
