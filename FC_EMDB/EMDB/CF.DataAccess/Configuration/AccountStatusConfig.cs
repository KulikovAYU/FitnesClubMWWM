using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
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
