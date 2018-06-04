using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
  public  class AbonementStatus : ViewModelBase
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
