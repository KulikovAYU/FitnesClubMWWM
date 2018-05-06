using System.Data.Entity.ModelConfiguration;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Данный класс представляет конфигурацию сущности "AccountStatus", предоставляемый Fluent API
    /// </summary>
    class AccountStatusConfig : EntityTypeConfiguration<AccountStatus>
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public AccountStatusConfig()
        {
            HasKey(accountStatus=> accountStatus.AccountStatusId);
            Property(accountStatus => accountStatus.AccountStatusName).HasMaxLength(20).HasColumnName("AccountStatusName");
            ToTable("AccountStatus");
        }
    }
}
