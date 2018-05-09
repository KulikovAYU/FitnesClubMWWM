using System.Data.Entity.ModelConfiguration;

namespace FitnesClubCL.CF_EMDB
{
    public class StatusTrainingConfig : EntityTypeConfiguration<StatusTraining>
    {
        public StatusTrainingConfig()
        {
            HasKey(statusTraining => statusTraining.StatusTarainingId);
            Property(statusTraining => statusTraining.StatusName).IsOptional().HasColumnName("statusTraining")
                .HasMaxLength(50);
        }
    }
}
