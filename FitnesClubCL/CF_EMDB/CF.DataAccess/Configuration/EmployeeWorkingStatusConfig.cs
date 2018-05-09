using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Threading;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Данный класс представлет конфигурацию сущности рабочий статус 
    /// </summary>
    public class EmployeeWorkingStatusConfig : EntityTypeConfiguration<EmployeeWorkingStatus>
    {
        public EmployeeWorkingStatusConfig()
        {
            HasKey(workingstatus => workingstatus.EmployeeWorkingStatusId);
            Property(workingstatus => workingstatus.EmployeeWorkingStatusName).IsOptional().HasMaxLength(50).HasColumnName("EmployeeWorkingStatusName");
            ToTable("EmployeeWorkingStatus");
        }
    }
}
