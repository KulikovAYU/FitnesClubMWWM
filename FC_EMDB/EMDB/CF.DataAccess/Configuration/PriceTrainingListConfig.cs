using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
{
    /// <summary>
    /// Данный класс представляет конфигурацию сущности "TrainingList", предоставляемый Fluent API
    /// </summary>
    class PriceTrainingListConfig : EntityTypeConfiguration<PriceTrainingList>
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public PriceTrainingListConfig()
        {
            HasKey(trainingList => trainingList.TrainingListId);
            Property(trainingList => trainingList.TrainingCurrentCost).HasColumnName("TrainingCurrentCost")
                .IsRequired().HasColumnType("money");

            //Property(trainingList => trainingList.CountTrainingList).HasColumnName("CountTrainingList")
            //    .IsRequired().HasColumnType("money");


            ToTable("TrainingList");
        }
    }
}
