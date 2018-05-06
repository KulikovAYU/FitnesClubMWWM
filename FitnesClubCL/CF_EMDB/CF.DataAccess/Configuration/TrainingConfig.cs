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
            Property(training => training.TrainingDuration).IsRequired().HasColumnName("TrainingDuration").
                HasColumnType("datetime2");
            Property(training => training.TrainingStartDate).IsRequired().HasColumnName("TrainingStartDate").
                HasColumnType("datetime2");
            Property(training => training.TrainingStartTime).IsRequired().HasColumnName("TrainingStartTime").
                HasColumnType("datetime2");
            Property(training => training.NumberOfSeats).IsRequired().HasColumnName("NumberOfSeats");
            Property(training => training.StatusTaraining).IsRequired().HasColumnName("StatusTaraining")
                .HasMaxLength(20);
            ToTable("Training");
        }
    }
}
