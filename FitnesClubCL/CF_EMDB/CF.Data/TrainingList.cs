namespace FitnesClubCL.CF_EMDB
{

    /// <summary>
    /// Сущность  список тренировок
    /// </summary>
    class TrainingList
    {
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
    }
}
