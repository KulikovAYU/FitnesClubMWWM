using System.Data.Entity.ModelConfiguration;

namespace FitnesClubCL.CF_EMDB
{
public class ServiceConfig : EntityTypeConfiguration<Service>
    {
        public ServiceConfig()
        {
            HasKey(service => service.ServiceId);
            Property(service => service.ServiceName).IsRequired().HasMaxLength(50).HasColumnName("ServiceName");
            ToTable("Services");
        }
    }
}
