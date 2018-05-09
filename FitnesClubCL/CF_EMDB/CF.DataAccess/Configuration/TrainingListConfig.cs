using System.Data.Entity.ModelConfiguration;

namespace FitnesClubCL.CF_EMDB
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
