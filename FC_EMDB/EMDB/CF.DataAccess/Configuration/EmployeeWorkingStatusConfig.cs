using System.Data.Entity.ModelConfiguration;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.DataAccess.Configuration
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
