using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
{
    /// <summary>
    /// Данный класс представляет конфигурацию сущности "Training", предоставляемый Fluent API
    /// </summary>
    class TrainingListConfig : EntityTypeConfiguration<TrainingList>
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public TrainingListConfig()
        {
            HasKey(trainingList => trainingList.TrainingListId);
            Property(trainingList => trainingList.TrainingCurrentCost).HasColumnName("TrainingCurrentCost")
                .IsRequired();
            Property(trainingList => trainingList.CountTrainingList).HasColumnName("CountTrainingList")
                .IsRequired().HasColumnType("money");

            ToTable("TrainingList");
        }
    }
}
