using System.Collections.Generic;

namespace FitnesClubCL.CF_EMDB
{
    public class StatusTraining
    {
        public StatusTraining()
        {
            Trainings = new HashSet<Training>();
        }
        /// <summary>
        /// Id статуса тренировки
        /// </summary>
        public int StatusTarainingId { get; set; }

        /// <summary>
        /// Описание статуса тренировки
        /// </summary>
        public string StatusName { get; set; }


        public ICollection<Training> Trainings { get; set; }
    }
}
