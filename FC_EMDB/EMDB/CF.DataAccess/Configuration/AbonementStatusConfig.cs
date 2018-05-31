using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
{
    class AbonementStatusConfig : EntityTypeConfiguration<AbonementStatus>
    {
        public AbonementStatusConfig()
        {
            HasKey(abonStatus => abonStatus.IdStatus);
            Property(abonStatus => abonStatus.StatusName).HasMaxLength(50).HasColumnName("StatusName");
            ToTable("AbonementStatus");
        }

    }
}
