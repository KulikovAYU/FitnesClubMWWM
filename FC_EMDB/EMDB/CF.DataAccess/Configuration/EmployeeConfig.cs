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
            HasKey(employee => employee.HumanId);
            Property(employee => employee.HumanFirstName).IsRequired().HasMaxLength(50).HasColumnName("EmployeeFirstName");
            Property(employee => employee.HumanLastName).IsRequired().HasMaxLength(50).HasColumnName("EmployeeLastName");
            Property(employee => employee.HumanFamilyName).IsRequired().HasMaxLength(50).HasColumnName("EmployeeFamilyName");
            Property(employee => employee.HumanDateOfBirdth).IsRequired().HasColumnName("EmployeeDateOfBirdth")
                .HasColumnType("datetime2");
            Property(employee => employee.HumanGender).IsOptional().HasMaxLength(50).HasColumnName("EmployeeGender");
            Property(employee => employee.HumanAdress).IsOptional().HasMaxLength(100)
                .HasColumnName("EmployeeAdress");
            Property(employee => employee.HumanPhoneNumber).IsRequired().HasMaxLength(50).HasColumnName("EmployeePhoneNumber");
            Property(employee => employee.HumanMail).IsOptional().HasColumnName("EmployeeMail").HasMaxLength(20);
            Property(employee => employee.HumanPhoto).IsOptional().HasColumnName("EmployeePhoto");

            Property(employee => employee.HumanPasportDataSeries).IsOptional().HasColumnName("EmployeePasportDataSeries").HasMaxLength(50);
            Property(employee => employee.HumanPasportDataNumber).IsOptional().HasColumnName("EmployeePasportDataNumber").HasMaxLength(50);
            Property(employee => employee.HumanPasportDataIssuedBy).IsOptional().HasColumnName("EmployeePasportDataIssuedBy").HasMaxLength(50);
            Property(employee => employee.HumanPasportDatеOfIssue).IsOptional().HasColumnName("EmployeePasportDatеOfIssue")
                .HasColumnType("datetime2");

            Property(employee => employee.EmployeeLoginName).IsRequired().HasMaxLength(50).HasColumnName("EmployeeLoginName");
            Property(employee => employee.EmployeePasswordHash).IsRequired().HasMaxLength(50)
                .HasColumnName("EmployeePasswordHash");

            ToTable("Employee");
        }
    }
}
