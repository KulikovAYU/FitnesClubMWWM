using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
{
    /// <summary>
    /// Данный класс представляет конфигурацию сущности "Account", предоставляемый Fluent API
    /// </summary>
    class AccountConfig : EntityTypeConfiguration<Account>
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public AccountConfig()
        {
            HasKey(account => account.HumanId);
            Property(account => account.HumanFirstName).IsRequired().HasColumnName("AccountFirstName").HasMaxLength(50);
            Property(account => account.HumanLastName).IsRequired().HasColumnName("AccountLastName").HasMaxLength(50);
            Property(account => account.HumanFamilyName).IsRequired().HasColumnName("AccountFamilyName")
                .HasMaxLength(50);
            Property(account => account.HumanDateOfBirdth).IsRequired().HasPrecision(15)
                .HasColumnName("ClientDateOfBirdth");
            Property(account => account.HumanGender).IsOptional().HasColumnName("AccountGender").HasMaxLength(15);
            Property(account => account.HumanAdress).IsOptional().HasColumnName("AccountAdress").HasMaxLength(100);
            Property(account => account.HumanPhoneNumber).IsRequired().HasColumnName("AccountPhoneNumber")
                .HasMaxLength(20);
            Property(account => account.HumanMail).IsOptional().HasColumnName("AccountMail").HasMaxLength(30);
            Property(account => account.HumanPhoto).IsOptional().HasColumnName("AccountPhoto");
            Property(account => account.HumanPasportDataSeries).IsRequired().HasMaxLength(10).HasColumnName("AccountPasportDataSeries");
            Property(account => account.HumanPasportDataNumber).IsRequired().HasMaxLength(10)
                .HasColumnName("HumanPasportDataNumber");
            Property(account => account.HumanPasportDataIssuedBy).IsRequired().HasMaxLength(30)
                .HasColumnName("HumanPasportDataIssuedBy");
            Property(account => account.HumanPasportDatеOfIssue).IsRequired().HasColumnName("AccountPasportDatеOfIssue").HasColumnType("datetime2");
            HasRequired(acc => acc.Abonement);
            

            //Property(account => account.AccountregistrationDate).IsRequired().HasColumnName("AccountregistrationDate").
            //    HasColumnType("datetime2");

            //Property(account => account.TrainingCount).IsOptional().HasColumnName("TrainingCount");
            //Property(account => account.VisitedTrainingCount).IsOptional().HasColumnName("VisitedTrainingCount");
            //Property(account => account.TotalCost).IsOptional().HasColumnName("TotalCost").HasColumnType("money"); ;
            //Property(account => account.NumberSubscription).IsRequired().HasColumnName("NumberSubscription");

            //Property(trainingList => trainingList.CountDays).HasColumnName("CountDays").IsOptional();
            //Property(trainingList => trainingList.AbonementActivationDateTime).HasColumnName("AbonementActivationDateTime").IsOptional()
            //    .HasColumnType("datetime2");
            ToTable("Account");
        }
    }
}
