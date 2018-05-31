using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
