using System.Collections.Generic;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Сущность статус отпуска
    /// </summary>
    public class EmployeeWorkingStatus
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
