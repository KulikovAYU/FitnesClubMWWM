using System.Collections.Generic;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Сущность тарифа
    /// </summary>
    public class Tarif
    {
        public Tarif()
        {
            ArrTrainingLists = new HashSet<TrainingList>();
        }
        /// <summary>
        /// Id тарифа
        /// </summary>
        public int TarifId { get; set; }

        /// <summary>
        /// Название тарифа
        /// </summary>
        public string TarifName { get; set; }


        public ICollection<TrainingList> ArrTrainingLists { get; set; }
    }
}
