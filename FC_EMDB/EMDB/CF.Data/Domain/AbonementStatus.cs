using System.Collections.Generic;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
  public  class AbonementStatus
    {
        public AbonementStatus()
        {
            ArrAbonements = new HashSet<Abonement>();
        }

        public int IdStatus { get; set; }
        public string StatusName { get; set; }

        public virtual  IEnumerable<Abonement> ArrAbonements { get; set; }

    }
}
