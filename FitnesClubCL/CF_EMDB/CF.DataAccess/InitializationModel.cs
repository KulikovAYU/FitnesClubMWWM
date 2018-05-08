using System;
using System.Data.Entity;
using FitnesClubCL.Utils;

namespace FitnesClubCL.CF_EMDB
{
    /// <summary>
    /// класс задает инициализацию БД данными
    /// </summary>
   class InitializationModel : DropCreateDatabaseAlways<DataBaseFcContext>
    {
        public InitializationModel()
        {

        }

        delegate int MyDelegate(DataBaseFcContext context);
        /// <summary>
        /// Метод переопределяется с целью фактического добавления данных в конекст для заполнения начальными значениями
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(DataBaseFcContext context)
        {
            base.Seed(context);

            if (context == null)
                return;
         
            var Employee1 = new Employee { EmployeeFirstName = "Иванов", EmployeeLastName = "Иван", EmployeeFamilyName = "Иванович", EmployeeAdress = "sd", EmployeePhoneNumber = "887", EmployeeLoginName = "work", EmployeePasswordHash = "work", EmployeeVacationStatus = "На работе" };
            Account account1 = new Account { NumberSubscription = AbonementGenerator.CreateNumberSubscription(context), AccountregistrationDate = DateTime.Now, TrainingCount = 10, VisitedTrainingCount = 5, TotalCost = 1000.0m, ClientFirstName = "Антон", ClientLastName = "Юрьевич", ClientFamilyName = "Куликов", ClientAdress = "г.Иваново, ул.Бакинский проезд, д.82, кв.11", ClientPhoneNumber = "8-920-672-00-68", ClientMail = "tosha37@inbox.ru"/*, ClientPhoto = ImageSQLController.ConvertToByteArray()*/, ClientPasportDataSeries = "2409", ClientPasportDataNumber = "460870", ClientPasportDataIssuedBy = "ОУФМС РОССИИ", ClientPasportDatеOfIssue = DateTime.Parse("01.07.2009"), ClientDateOfBirdth = DateTime.Parse("27.05.1989"), Employee = Employee1 };
            Account account2 = new Account { NumberSubscription = AbonementGenerator.CreateNumberSubscription(context), AccountregistrationDate = DateTime.Now, TrainingCount = 20, VisitedTrainingCount = 15, TotalCost = 3000.0m, ClientFirstName = "Яна", ClientLastName = "Алексеевна", ClientFamilyName = "Куликова", ClientAdress = "г.Иваново, ул.Бакинский проезд, д.82, кв.11", ClientPhoneNumber = "8-920-672-00-68", ClientMail = "tosha37@inbox.ru"/*, ClientPhoto = ImageSQLController.ConvertToByteArray()*/, ClientPasportDataSeries = "2409", ClientPasportDataNumber = "460870", ClientPasportDataIssuedBy = "ОУФМС РОССИИ", ClientPasportDatеOfIssue = DateTime.Parse("01.07.2009"), ClientDateOfBirdth = DateTime.Parse("27.05.1989"), Employee = Employee1 };
            context.Accounts.Add(account1);
            context.Accounts.Add(account2);
            context.SaveChanges();
        }
    }
}
