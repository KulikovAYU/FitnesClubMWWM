using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
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
            //var query = from training in DataBaseFcContext.Trainings
            //    where training.TrainingId == nTrainingId
            //    select training.Employee;

            var query = DataBaseFcContext.Trainings.Where(train => train.TrainingId == nTrainingId)
                .Select(emp => emp.Employee).FirstOrDefault();
            return query;
        }

        /// <summary>
        /// Получить работника, ведущего определенную тренировку
        /// </summary>
        /// <param name="strTrainingName">Имя тренировки</param>
        /// <param name="starDateTime">Дата начала</param>
        /// <returns>Экземпляр работника, ведущего определенную тренировку</returns>
        public Employee GetEmployeeLeadingTraining(string strTrainingName, DateTime starDateTime)
        {
            //var query = from training in DataBaseFcContext.Trainings
            //    where training.Service.ServiceName == strTrainingName && training.TrainingDateTime == starDateTime
            //    select training.Employee;
            var query = DataBaseFcContext.Trainings.Where(train => train.TrainingDateTime == starDateTime).Where(train=> train.Service.ServiceName == strTrainingName)
                .Select(emp => emp.Employee).FirstOrDefault();
            return query;
        }

        /// <summary>
        /// Получить работников с конкретной ролью
        /// </summary>
        /// <param name="strRole">статус работников</param>
        /// <returns>Коллекция работников с определенной ролью</returns>
        public IEnumerable<Employee> GetEmployeesWithConcreteRole(string strRole)
        {
            //var query = from employee in DataBaseFcContext.EmployeeRoles
            //    where employee.EmployeeRoleName.Equals(strRole)
            //    select employee.ArrEmployees;
           
            var query = DataBaseFcContext.EmployeeRoles.Where(role => role.EmployeeRoleName == strRole)
                .Select(emp => emp.ArrEmployees).FirstOrDefault();

            return query;
        }

        /// <summary>
        /// Получить пользователя по имени пользователя и паролю
        /// </summary>
        /// <param name="strUserName">Имя пользователя</param>
        /// <param name="strPasswordHash">Пароль</param>
        /// <returns>Сущность работника</returns>
        public Employee GetEmployeeByAutorizationUserData(string strUserName, string strPasswordHash)
        {
            
              //var query = DataBaseFcContext.Employees.FirstOrDefault(employee => employee.EmployeeLoginName == strUserName);
              var query = DataBaseFcContext.Employees
                .Where(employee => employee.EmployeeLoginName == strUserName).FirstOrDefault(employee => employee.EmployeePasswordHash == strPasswordHash);

            return query;
        }

        /// <summary>
        /// Получить фотографию работника
        /// </summary>
        /// <param name="employee">Сущность работника</param>
        /// <returns>Фотографию пользователя информационной системы</returns>
        public Image GetSystemUserPhoto(Employee employee)
        {
            if (employee == null)
                return null;

            var query = DataBaseFcContext.Employees.Where(empl => empl.EmployeeId == employee.EmployeeId)
                .Select(img => img.EmployeePhoto);
            if (query.FirstOrDefault() == null)
                return null;

            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(query.First(), 0, query.First().Length);
            Image currentImage = Image.FromStream(memoryStream);
            return currentImage;
        }

        /// <summary>
        /// Получить фотографию работника в виде массива байт
        /// </summary>
        /// <param name="employee">Сущность работника</param>
        /// <returns>фото пользователя инфосистемы в виде массива байт</returns>
        public byte[] GetSystemUserByreArrPhoto(Employee employee)
        {
            if (employee == null)
                return null;
            var query = DataBaseFcContext.Employees.Where(empl => empl.EmployeeId == employee.EmployeeId)
                .Select(img => img.EmployeePhoto);

            return query.FirstOrDefault();
        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
