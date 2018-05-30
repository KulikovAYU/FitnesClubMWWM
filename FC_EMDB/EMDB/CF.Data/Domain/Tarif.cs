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
            ArrTrainingLists = new HashSet<PriceTrainingList>();
        }
        /// <summary>
        /// Id тарифа
        /// </summary>
        public int TarifId { get; set; }

        /// <summary>
        /// Название тарифа
        /// </summary>
        public string TarifName { get; set; }

        /// <summary>
        /// Корректирующий коэффициент тарифа на утренний коэффициент 0,7 например. Захардкодил
        /// </summary>
        public decimal Koeff => TarifName == "Вечерний" ? 1.0m : 0.7m;

        public ICollection<PriceTrainingList> ArrTrainingLists { get; set; }
    }
}
