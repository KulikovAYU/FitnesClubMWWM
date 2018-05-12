using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnesClubCL.CF_EMDB.Repositories;

namespace FitnesClubCL.CF_EMDB
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get;  }
        IEmployeeRepository Employess { get; }

        int Complete();
    }
}
