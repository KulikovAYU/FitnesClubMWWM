using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
{
    public class StatusTrainingConfig : EntityTypeConfiguration<StatusTraining>
    {
        public StatusTrainingConfig()
        {
            HasKey(statusTraining => statusTraining.StatusTarainingId);
            Property(statusTraining => statusTraining.StatusName).IsOptional().HasColumnName("statusTraining")
                .HasMaxLength(50);
            ToTable("StatusTrainingConfig");
        }
    }
}
