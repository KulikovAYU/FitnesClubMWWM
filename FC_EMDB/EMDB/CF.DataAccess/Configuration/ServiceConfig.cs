using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
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
