namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Сущность зал
    /// </summary>
    class Gym
    {
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
    }
}
