using System;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Описание сущности "тренировка"
    /// </summary>
    class Training
    {
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

    }
}
