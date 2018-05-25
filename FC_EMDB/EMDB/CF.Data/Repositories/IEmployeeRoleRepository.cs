using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.Data.Repositories
{
    public interface IEmployeeRoleRepository : IRepository<EmployeeRole>
    {
        /// <summary>
        /// Получить роль пользователя
        /// </summary>
        /// <param name="employee">Экземпляр работнитка</param>
        /// <returns>Роль конкретного работника</returns>
        EmployeeRole GetRole(Employee employee);

        /// <summary>
        /// Вернуть имя роли
        /// </summary>
        /// <param name="employee">Экземпляр работнитка</param>
        /// <returns>Имя роли</returns>
        string GetRoleName(Employee employee);
    }
}
