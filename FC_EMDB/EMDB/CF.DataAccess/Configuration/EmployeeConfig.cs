using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
{
    /// <summary>
    /// Данный класс представляет конфигурацию сущности "Employee", предоставляемый Fluent API
    /// </summary>
    public class EmployeeConfig : EntityTypeConfiguration<Employee>
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public EmployeeConfig()
        {
            HasKey(employee => employee.EmployeeId);
            Property(employee => employee.EmployeeFirstName).IsRequired().HasMaxLength(50).HasColumnName("EmployeeFirstName");
            Property(employee => employee.EmployeeLastName).IsRequired().HasMaxLength(50).HasColumnName("EmployeeLastName");
            Property(employee => employee.EmployeeFamilyName).IsRequired().HasMaxLength(50).HasColumnName("EmployeeFamilyName");
            Property(employee => employee.EmployeeDateOfBirdth).IsOptional().HasColumnName("EmployeeDateOfBirdth")
                .HasColumnType("datetime2");
            Property(employee => employee.EmployeeAdress).IsRequired().HasMaxLength(50).HasColumnName("EmployeeAdress");
            Property(employee => employee.EmployeePhoneNumber).IsRequired().HasMaxLength(20)
                .HasColumnName("PhoneNumber");
            Property(employee => employee.EmployeeMail).IsOptional().HasMaxLength(50).HasColumnName("EmployeeE-mail");
            Property(employee => employee.EmployeePhoto).IsOptional().HasColumnName("EmployeePhoto").HasColumnType("image");
            Property(employee => employee.EmployeeLoginName).IsRequired().HasMaxLength(50).HasColumnName("EmployeeLoginName");
            Property(employee => employee.EmployeePasswordHash).IsRequired().HasMaxLength(50)
                .HasColumnName("EmployeePasswordHash");

            ToTable("Employee");
        }
    }
}
