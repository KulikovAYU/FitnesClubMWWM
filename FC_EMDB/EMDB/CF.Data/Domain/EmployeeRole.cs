using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
    /// <summary>
    /// Роль(должность) работника
    /// </summary>
    public class EmployeeRole : ViewModelBase
    {
        public EmployeeRole()
        {
            ArrEmployees = new HashSet<Employee>();
        }
        public int EmployeeRoleId { get; set; }
        /// <summary>
        /// Название роли(должности)
        /// </summary>
        public string EmployeeRoleName { get; set; }

        /// <summary>
        /// Размер оклада
        /// </summary>
        public Decimal EmployeeSalaryValue { get; set; }

        #region Связи между сущностями 1..1 EmployeeRole to 0...* Employee

        /// <summary>
        /// Свойство навигации (Virtual ->Lazy Load)
        /// </summary>
        public virtual ICollection<Employee> ArrEmployees { get; set; }
        #endregion

    }
}
