using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnesClubCL.CF_EMDB.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataBaseFcContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить работника, ведущего определенную тренировку
        /// </summary>
        /// <param name="nTrainingId"> id тренировки</param>
        /// <returns>Экземпляр работника, ведущего определенную тренировку</returns>
        public Employee GetEmployeeLeadingTraining(int nTrainingId)
        {
            var query = from training in DataBaseFcContext.Trainings
                where training.TrainingId == nTrainingId
                select training.Employee;

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Получить работника, ведущего определенную тренировку
        /// </summary>
        /// <param name="strTrainingName">Имя тренировки</param>
        /// <param name="starDateTime">Дата начала</param>
        /// <returns>Экземпляр работника, ведущего определенную тренировку</returns>
        public Employee GetEmployeeLeadingTraining(string strTrainingName, DateTime starDateTime)
        {
            var query = from training in DataBaseFcContext.Trainings
                where training.Service.ServiceName == strTrainingName && training.TrainingDateTime == starDateTime
                select training.Employee;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Получить работников с конкретной ролью
        /// </summary>
        /// <param name="strRole">статус работников</param>
        /// <returns>Коллекция работников с определенной ролью</returns>
        public IEnumerable<Employee> GetEmployeesWithConcreteRole(string strRole)
        {
            var query = from employee in DataBaseFcContext.EmployeeRoles
                where employee.EmployeeRoleName.Equals(strRole)
                select employee.ArrEmployees;
            return query.FirstOrDefault();
        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
