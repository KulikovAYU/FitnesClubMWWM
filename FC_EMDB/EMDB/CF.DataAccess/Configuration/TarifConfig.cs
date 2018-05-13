using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
{
    public class TarifConfig : EntityTypeConfiguration<Tarif>
    {
        public TarifConfig()
        {
            HasKey(tarif => tarif.TarifId);
            Property(tarif => tarif.TarifName).IsRequired().HasMaxLength(50).HasColumnName("TarifName");
            ToTable("Tarif");
        }
    }
}
