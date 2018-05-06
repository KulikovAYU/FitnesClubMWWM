using System.Data.SqlTypes;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// Сущность зарплаты
    /// </summary>
    class Salary
    {
        /// <summary>
        /// Id зарплаты
        /// </summary>
        public int SalaryId { get; set; }

        /// <summary>
        /// Величина зарплаты
        /// </summary>
        public SqlMoney SalaryValue { get; set; }
    }
}
