using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Сущность зарплаты
    /// </summary>
   public class Salary
    {
        public Salary()
        {
            ArrEmployees = new HashSet<Employee>();
        }
        /// <summary>
        /// Id зарплаты
        /// </summary>
        public int SalaryId { get; set; }

        /// <summary>
        /// Величина зарплаты
        /// </summary>
        public Decimal SalaryValue { get; set; }

        #region Связи между сущностями 1..1 Salary to 0...* Employee

        /// <summary>
        /// Свойство навигации (Virtual ->Lazy Load)
        /// </summary>
        public virtual ICollection<Employee> ArrEmployees { get; set; }
        #endregion
    }
}
