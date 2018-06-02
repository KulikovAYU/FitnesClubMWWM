using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
{
    class UpcomingTrainingConfig : EntityTypeConfiguration<UpcomingTraining>
    {
        public UpcomingTrainingConfig()
        {
            HasKey(upcTrain => upcTrain.TrainingId);
            Property(upcTrain => upcTrain.NumberOfSeats).IsRequired().HasColumnName("upcTrain");
            Property(upcTrain => upcTrain.TrainingDateTime).IsRequired().HasColumnName("TrainingDateTime")
                .HasColumnType("datetime2");
            ToTable("UpcomingTraining");

        }
    }
}
