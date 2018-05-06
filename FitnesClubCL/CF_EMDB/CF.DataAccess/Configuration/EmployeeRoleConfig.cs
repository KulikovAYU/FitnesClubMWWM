using System.Data.Entity.ModelConfiguration;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Данный класс представляет конфигурацию сущности "Gym", предоставляемый Fluent API
    /// </summary>
    class EmployeeRoleConfig : EntityTypeConfiguration<EmployeeRole> 
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public EmployeeRoleConfig()
        {
            HasKey(employeeRole => employeeRole.EmployeeRoleId);
            Property(employeeRole => employeeRole.EmployeeRoleName).HasMaxLength(50).HasColumnName("EmployeeRoleName");
        }
    }
}
