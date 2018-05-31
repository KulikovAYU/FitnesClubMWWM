using System;
using System.Data.Entity;
using System.Linq;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
   public class EmployeeWorkingStatusRepository : Repository<EmployeeWorkingStatus>, IEmployeeWorkingStatusRepository
    {
        public EmployeeWorkingStatusRepository(DataBaseFcContext context) : base(context)
        {
        }

        public string GetEmployeeWorkingStatus(Employee employee)
        {
            if (employee == null)
                return null;
            var query = DataBaseFcContext.Employees.Where(emp => emp.HumanId == employee.HumanId)
                .Select(workingStatus => workingStatus.EmployeeWorkingStatus.EmployeeWorkingStatusName)
                .FirstOrDefault();
            return query;
        }
        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
