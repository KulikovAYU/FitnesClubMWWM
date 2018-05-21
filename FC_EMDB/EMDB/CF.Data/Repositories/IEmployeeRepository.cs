using System;
using System.Collections.Generic;
using System.Drawing;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnesClubCL.CF_EMDB.Repositories;

namespace FC_EMDB.EMDB.CF.Data.Repositories
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
        Employee GetEmployeeLeadingTraining(string strTrainingName, DateTime starDateTime);


        /// <summary>
        /// Получить работников с конкретной ролью
        /// </summary>
        /// <param name="strRole">статус работников</param>
        /// <returns>Коллекция работников с определенной ролью</returns>
        IEnumerable<Employee> GetEmployeesWithConcreteRole(string strRole);

        /// <summary>
        /// Получить работника по имени пользователя и паролю
        /// </summary>
        /// <param name="strUserName">Имя пользователя</param>
        /// <param name="strPasswordHash">Хэш пароля</param>
        /// <returns></returns>
        Employee GetEmployeeByAutorizationUserData(string strUserName, string strPasswordHash);


        /// <summary>
        /// Получить фото пользователя информационной системы
        /// </summary>
        /// <param name="employee">Экземпляр работника</param>
        /// <returns>Изображение пользователя информационной системы</returns>
        Image GetSystemUserPhoto(Employee employee);

        /// <summary>
        /// Получить изображение в виде массива байтов
        /// </summary>
        /// <param name="employee">Экземпляр работника</param>
        /// <returns>Байты изображения пользователя информационной системы</returns>
        byte[] GetSystemUserByreArrPhoto(Employee employee);
    }
}
