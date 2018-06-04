using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Сущность статус отпуска
    /// </summary>
    public class EmployeeWorkingStatus : ViewModelBase
    {
        public EmployeeWorkingStatus()
        {
            Employees = new HashSet<Employee>();
        }

        /// <summary>
        /// Id статуса работника
        /// </summary>
        public int EmployeeWorkingStatusId { get; set; }

        public string EmployeeWorkingStatusName { get; set; }

        /// <summary>
        /// Свойство навигации
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
