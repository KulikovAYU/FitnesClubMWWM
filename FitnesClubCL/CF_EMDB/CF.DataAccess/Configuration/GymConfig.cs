using System.Data.Entity.ModelConfiguration;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Данный класс представляет конфигурацию сущности "Gym", предоставляемый Fluent API
    /// </summary>
    class GymConfig : EntityTypeConfiguration<Gym>
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public GymConfig()
        {
            HasKey(gym => gym.GymId);
            Property(gym => gym.GymName).IsRequired().HasMaxLength(50).HasColumnName("GymName");
            Property(gym => gym.GymCapacity).IsRequired().HasColumnName("GymCapacity");
            ToTable("Gym");
        }
    }
}
