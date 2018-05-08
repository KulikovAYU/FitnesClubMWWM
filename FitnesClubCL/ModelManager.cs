using System;
using System.Data.Entity;
using System.Linq;
using FitnesClubCL.CF_EMDB;
using FitnesClubCL.Utils;

namespace FitnesClubCL
{
   public static class ModelManager
    {
        delegate int MyDelegate(DataBaseFcContext context);
        /// <summary>
        /// Метод создает БД
        /// </summary>
        public static void CreateDb()
        {
            
            using (var db = new DataBaseFcContext("dbContext"))
            {
                //TODO: Внимание, для того, чтобы сработала дефолтная инициализация, необходимо прописать тут хоть одну инициализацию
                //анонимный метод, генерирующий абонемент
             
                var _Employee = new Employee { EmployeeFirstName = "errgawe", EmployeeLastName = "Иван", EmployeeFamilyName = "Иванович", EmployeeAdress = "sd", EmployeePhoneNumber = "887", EmployeeLoginName = "work", EmployeePasswordHash = "work", EmployeeVacationStatus = "На работе" };
                Account account = new Account { NumberSubscription = AbonementGenerator.CreateNumberSubscription(db), AccountregistrationDate = DateTime.Now, TrainingCount = 10, VisitedTrainingCount = 5, TotalCost = 1000.0m, ClientFirstName = "fgfdgfd", ClientLastName = "Юрьевич", ClientFamilyName = "Куликов", ClientAdress = "г.Иваново, ул.Бакинский проезд, д.82, кв.11", ClientPhoneNumber = "8-920-672-00-68", ClientMail = "tosha37@inbox.ru"/*, ClientPhoto = ImageSQLController.ConvertToByteArray()*/, ClientPasportDataSeries = "2409", ClientPasportDataNumber = "460870", ClientPasportDataIssuedBy = "ОУФМС РОССИИ", ClientPasportDatеOfIssue = DateTime.Parse("01.07.2009"), ClientDateOfBirdth = DateTime.Parse("27.05.1989"), Employee = _Employee };
                db.Accounts.Add(account);
                db.SaveChanges();

            }
        }
    }
}
