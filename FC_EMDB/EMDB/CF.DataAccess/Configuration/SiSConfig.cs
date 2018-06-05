using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
{
    public class SiSConfig : EntityTypeConfiguration<ServicesInSubscription>
    {
        public SiSConfig()
        {
            HasKey(service => service.SiSId);
           // Property(service => service.SiSType).IsRequired().HasColumnName("ServicesInSubscriptionName");
            Property(service => service.SiSTrainingCount).IsOptional().HasColumnName("SiSTrainingCount");
            Property(service => service.SiSVisitedTrainingCount).IsRequired().HasColumnName("SiSVisitedTrainingCount");
              Property(service => service.SiSTrainingName).IsOptional().HasColumnName("SiSTrainingName");
            Property(service => service.TotalCost).IsRequired().HasColumnName("TotalCost");

            ToTable("ServicesInSubscription");
        }
    }
}
