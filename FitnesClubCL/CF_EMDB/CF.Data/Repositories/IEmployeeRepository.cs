using System;
using System.Collections.Generic;
using FitnesClubCL.Annotations;

namespace FitnesClubCL.CF_EMDB.Repositories
{
    /// <summary>
    /// Представляет интерфейс репозитория для рабоника
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Получить работника, ведущего определенную тренировку
        /// </summary>
        /// <param name="nTrainingId"> id тренировки</param>
        /// <returns>Экземпляр работника, ведущего определенную тренировку</returns>
        Employee GetEmployeeLeadingTraining(int nTrainingId);

        /// <summary>
        /// Получить работника, ведущего определенную тренировку
        /// </summary>
        /// <param name="strTrainingName">имя тренировки</param>
        /// <param name="starDateTime">дата тренировки</param>
        /// <returns>Экземпляр работника, ведущего определенную тренировку</returns>
        Employee GetEmployeeLeadingTraining([CanBeNull] string strTrainingName, DateTime starDateTime);


        /// <summary>
        /// Получить работников с конкретной ролью
        /// </summary>
        /// <param name="strRole">статус работников</param>
        /// <returns>Коллекция работников с определенной ролью</returns>
        IEnumerable<Employee> GetEmployeesWithConcreteRole(string strRole);
    }
}
