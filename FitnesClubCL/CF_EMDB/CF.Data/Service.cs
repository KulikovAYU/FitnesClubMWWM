﻿using System.Collections.Generic;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Сущность "услуга"
    /// </summary>
    public class Service
    {
        public Service()
        {
            Trainings = new HashSet<Training>();
        }
        /// <summary>
        /// Id услуги
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Название услуги
        /// </summary>
        public string ServiceName { get; set; }

      
        public ICollection<Training> Trainings { get; set; }
    }
}
