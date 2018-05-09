using System.Data.Entity.ModelConfiguration;

namespace FitnesClubCL.CF_EMDB
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
