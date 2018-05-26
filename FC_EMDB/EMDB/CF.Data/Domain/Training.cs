using System;
using System.Collections.Generic;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Описание сущности "тренировка", которая предстоит
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
        /// Год, месяц, день, часы, минуты, секунды : Продолжительность тренировки, Дата тренировки,Время начала тренировки
        /// </summary>
        public DateTime TrainingDateTime { get; set; }

        /// <summary>
        /// Количество мест
        /// </summary>
        public int NumberOfSeats { get; set; }


        /// <summary>
        /// Статус трениорвки: Закончена или идет запись, группа набрана
        /// </summary>
        #region Связи между сущностями 1...1 StatusTraining to 0...* Training
        public StatusTraining StatusTraining { get; set; }
        #endregion

        #region Связи между сущностями 0...* Account to 0...* Training

        public ICollection<Account> ArrAccounts { get; set; }

        #endregion

        #region Связи между сущностями 0...* Training to 1 Service

        public virtual Service Service { get; set; }

        #endregion

        #region Связи между сущностями 0...* Training to 1 Gym

        public Gym Gym { get; set; }

        #endregion

        #region Связи между сущностями 0..1 Employee to 0...* Training

        /// <summary>
        /// Свойство навигации (Virtual ->Lazy Load)
        /// </summary>
        public Employee Employee { get; set; }
        #endregion
    }
}
