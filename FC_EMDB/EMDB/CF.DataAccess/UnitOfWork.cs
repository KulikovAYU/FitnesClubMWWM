using FC_EMDB.EMDB.CF.Data;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;
using FC_EMDB.EMDB.CF.DataAccess.Repositories;

namespace FC_EMDB.EMDB.CF.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseFcContext m_context;
        public UnitOfWork(DataBaseFcContext context)
        {
            m_context = context;
            //иницилизация репозиториев
            Accounts = new AccountRepository(m_context);
            Employess = new EmployeeRepository(m_context);
            EmployeRoles = new EmployeeRoleRepository(m_context);
        }

        public IAccountRepository Accounts { get; private set; }
        public IEmployeeRepository Employess { get; private set; }
        public IEmployeeRoleRepository EmployeRoles { get; private set; }
       

        public int Complete()
        {
            return m_context.SaveChanges();
        }
        public void Dispose()
        {
            m_context.Dispose();
        }
    }
}
