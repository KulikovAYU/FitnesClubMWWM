﻿using System.Collections.Generic;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Сущность  список тренировок и услуг
    /// </summary>
    public class TrainingList
    {
        public TrainingList()
        {
            Accounts = new HashSet<Account>();
        }
        /// <summary>
        /// Id списка тренировок
        /// </summary>
        public int TrainingListId { get; set; }

        /// <summary>
        /// Количество тренировок
        /// </summary>
        public int CountTrainingList { get; set; }

        /// <summary>
        /// Стоимость тренировки
        /// </summary>
        public decimal TrainingCurrentCost { get; set; }
        
        #region Связи между сущностями 0...* TrainingList to 1 Tarif
        public Tarif Tarifs { get; set; }
        #endregion

        #region Связи между сущностями 1 TrainingList to 1 Service
        public Service TrainingListName { get; set; }
        #endregion

        #region Связи между сущностями 0...* Accounts to 0...* TrainingList
        public ICollection<Account> Accounts { get; set; }
        #endregion
    }
}
