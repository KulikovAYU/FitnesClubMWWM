using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
