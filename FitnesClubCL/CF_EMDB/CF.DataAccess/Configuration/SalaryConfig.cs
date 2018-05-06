using System.Data.Entity.ModelConfiguration;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Данный класс представляет конфигурацию сущности "SalaryConfig", предоставляемый Fluent API
    /// </summary>
    class SalaryConfig : EntityTypeConfiguration<Salary>
    { 
        /// <summary>
        /// Конфигурация
        /// </summary>
        public SalaryConfig()
        {
            HasKey(salary => salary.SalaryId);
            Property(salary => salary.SalaryValue).HasColumnName("SalaryValue");
            ToTable("Salary");
        }
    }
}
