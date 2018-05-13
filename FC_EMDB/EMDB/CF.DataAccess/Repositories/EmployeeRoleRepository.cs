using System.Linq;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
   public class EmployeeRoleRepository : Repository<EmployeeRole>, IEmployeeRoleRepository
    {
        public EmployeeRoleRepository(DataBaseFcContext context) : base(context)
        {
        }

        public EmployeeRole GetRole(Employee employee)
        {
            if (employee == null)
                return null;
            var query = DataBaseFcContext.Employees.Where(emp => emp.EmployeeId == employee.EmployeeId)
                .Select(role => role.EmployeeRole).FirstOrDefault();
            return query;
        }

        public string GetRoleName(Employee employee)
        {
            if (employee == null)
                return null;
            var query = DataBaseFcContext.Employees.Where(emp => emp.EmployeeId == employee.EmployeeId)
                .Select(role => role.EmployeeRole.EmployeeRoleName).FirstOrDefault();
            return query;
        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
