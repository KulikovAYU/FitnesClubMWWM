using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
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
