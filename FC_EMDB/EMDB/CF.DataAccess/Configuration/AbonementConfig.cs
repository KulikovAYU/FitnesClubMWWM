using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
{
    class AbonementConfig : EntityTypeConfiguration<Abonement>
    {
        public AbonementConfig()
        {
            HasKey(abon => abon.AbonementId);

            Property(abon => abon.NumberSubscription).IsRequired().HasColumnName("NumberSubscription");
            Property(abon => abon.AbonementregistrationDate).IsRequired().HasColumnName("AbonementregistrationDate").HasColumnType("datetime2");
            Property(abon => abon.TrainingCount).IsRequired().HasColumnName("TrainingCount");
            Property(abon => abon.VisitedTrainingCount).IsRequired().HasColumnName("VisitedTrainingCount");
            Property(abon => abon.TotalCost).IsRequired().HasColumnName("TotalCost").HasColumnType("money");
            Property(abon => abon.CountDays).IsOptional().HasColumnName("CountDays").HasColumnType("datetime2");
            Property(abon => abon.AbonementActivationDateTime).IsRequired().HasColumnName("AbonementActivationDateTime").HasColumnType("datetime2");
            Property(abon => abon.TimeToLong).IsOptional().HasColumnName("TimeToLong").HasColumnType("datetime2");
            ToTable("Abonement");
        }
    }
}
