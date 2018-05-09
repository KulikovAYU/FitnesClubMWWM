using System.Data.Entity.ModelConfiguration;

namespace FitnesClubCL.CF_EMDB
{ /// <summary>
  /// Данный класс представляет конфигурацию сущности "Training", предоставляемый Fluent API
  /// </summary>
    class TrainingConfig : EntityTypeConfiguration<Training>
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public TrainingConfig()
        {
            HasKey(training=> training.TrainingId);
            Property(training => training.TrainingDateTime).IsRequired().HasColumnName("TrainingDateTime").
                HasColumnType("datetime2");
            Property(training => training.NumberOfSeats).IsRequired().HasColumnName("NumberOfSeats");
           ToTable("Training");
        }
    }
}
