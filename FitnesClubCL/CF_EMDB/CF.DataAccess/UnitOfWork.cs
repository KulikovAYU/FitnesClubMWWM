using FitnesClubCL.CF_EMDB.Repositories;

namespace FitnesClubCL.CF_EMDB
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
        }

        public IAccountRepository Accounts { get; private set; }
        public IEmployeeRepository Employess { get; private set; }

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
