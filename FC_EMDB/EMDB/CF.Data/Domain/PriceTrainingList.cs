using System.Collections.Generic;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Сущность  список тренировок и услуг(прайс-лист)
    /// </summary>
    public class PriceTrainingList
    {
        public PriceTrainingList()
        {
            Accounts = new HashSet<Account>();
        }
        /// <summary>
        /// Id списка тренировок
        /// </summary>
        public int TrainingListId { get; set; }

        ///// <summary>
        ///// Количество тренировок или услуг
        ///// </summary>
        //public int CountTrainingList { get; set; }

        /// <summary>
        /// Стоимость тренировки
        /// </summary>
        public decimal TrainingCurrentCost { get; set; }

        #region Связи между сущностями 0...* TrainingList to 1 Tarif
        public virtual Tarif Tarifs { get; set; }
        #endregion

        #region Связи между сущностями 1 TrainingList to 1 Service
        public virtual Service TrainingListName { get; set; }
        #endregion

        #region Связи между сущностями 0...* Accounts to 0...* TrainingList Список работника, ведущего тренировку
        public virtual ICollection<Account> Accounts { get; set; }
        #endregion

     
    }
}
