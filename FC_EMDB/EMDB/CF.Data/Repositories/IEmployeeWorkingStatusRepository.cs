﻿using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.EMDB.CF.Data.Repositories
{
   public interface IEmployeeWorkingStatusRepository : IRepository<EmployeeWorkingStatus>
   {
       /// <summary>
       /// Получить рабочий статус работгника
       /// </summary>
       /// <param name="employee">сущность работника</param>
       /// <returns>Имя рабочего статуса</returns>
       string GetEmployeeWorkingStatus(Employee employee);
   }
}
