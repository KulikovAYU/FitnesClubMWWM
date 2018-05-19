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
            HasKey(account => account.ClientId);
            Property(account => account.AccountregistrationDate).IsRequired().HasColumnName("AccountregistrationDate").
                HasColumnType("datetime2");

            Property(account => account.TrainingCount).IsRequired().HasColumnName("TrainingCount");
            Property(account => account.VisitedTrainingCount).IsRequired().HasColumnName("VisitedTrainingCount");
            Property(account => account.TotalCost).IsRequired().HasColumnName("TotalCost").HasColumnType("money"); ;
            Property(account => account.NumberSubscription).IsRequired().HasColumnName("NumberSubscription");
            Property(account => account.ClientFirstName).IsRequired().HasColumnName("ClientFirstName").HasMaxLength(50);
            Property(account => account.ClientLastName).IsRequired().HasColumnName("ClientLastName").HasMaxLength(50);
            Property(account => account.ClientFamilyName).IsRequired().HasColumnName("ClientFamilyName")
                .HasMaxLength(50);
            Property(account => account.ClientDateOfBirdth).IsRequired().HasPrecision(15)
                .HasColumnName("ClientDateOfBirdth");
            Property(account => account.ClientAdress).IsRequired().HasColumnName("ClientAdress").HasMaxLength(100);
            Property(account => account.ClientPhoneNumber).IsRequired().HasColumnName("ClientPhoneNumber")
                .HasMaxLength(20);
            Property(account => account.ClientMail).IsOptional().HasColumnName("ClientMail").HasMaxLength(30);
            Property(account => account.ClientPhoto).IsOptional().HasColumnName("ClientPhoto");
            Property(account => account.ClientPasportDataSeries).IsRequired().HasMaxLength(10).HasColumnName("ClientPasportDataSeries");
            Property(account => account.ClientPasportDataNumber).IsRequired().HasMaxLength(10)
                .HasColumnName("ClientPasportDataNumber");
            Property(account => account.ClientPasportDataIssuedBy).IsRequired().HasMaxLength(30)
                .HasColumnName("ClientPasportDataIssuedBy");
            Property(account => account.ClientPasportDatеOfIssue).IsRequired().HasColumnName("ClientPasportDatеOfIssue").HasColumnType("datetime2");
            Property(account => account.ClientGender).IsRequired().HasColumnName("ClientGender").HasMaxLength(15);
          ToTable("Account");
        }
    }
}
