using System;
using FC_EMDB.EMDB.CF.Data.Repositories;

namespace FC_EMDB.EMDB.CF.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get;  }
        IEmployeeRepository Employess { get; }
        IEmployeeRoleRepository EmployeRoles { get; }
        IEmployeeWorkingStatusRepository EmployeesWorkingStatus { get;}

        int Complete();
    }
}
