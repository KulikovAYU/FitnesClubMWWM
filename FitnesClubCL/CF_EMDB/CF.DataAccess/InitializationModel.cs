using System;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// Статус работника eWorking - на работе, eSick -болен, eVacation -в отпуске
        /// </summary>
        enum eWorkingStaus
        {
            eWorking, eSick, eVacation
        }
        delegate int MyDelegate(DataBaseFcContext context);
        /// <summary>
        /// Метод переопределяется с целью фактического добавления данных в конекст для заполнения начальными значениями
        /// </summary>
        /// <param name="context"></param>
        protected  override void Seed(DataBaseFcContext context)
        {
            base.Seed(context);

            if (context == null)
                return;

            #region Добавление справочника должностей с зарплатами
            ObservableCollection<EmployeeRole> roles = new ObservableCollection<EmployeeRole>
            {
                new EmployeeRole {EmployeeRoleName = "Администратор", EmployeeSalaryValue=27000.0m},
                new EmployeeRole {EmployeeRoleName = "Фитнес-инструктор", EmployeeSalaryValue=30000.0m},
                new EmployeeRole {EmployeeRoleName = "Менеджер", EmployeeSalaryValue=25000.0m},
                new EmployeeRole {EmployeeRoleName = "Руководитель", EmployeeSalaryValue=50000.0m}
            };
            context.EmployeeRoles.AddRange(roles);
            #endregion

            #region Добавление статусов работников фитнес-клуба
            ObservableCollection<EmployeeWorkingStatus> employeeWorkingStatuses =
                new ObservableCollection<EmployeeWorkingStatus>
                {
                    new EmployeeWorkingStatus{EmployeeWorkingStatusName = "На работе"},
                    new EmployeeWorkingStatus{EmployeeWorkingStatusName = "На больничном"},
                    new EmployeeWorkingStatus{EmployeeWorkingStatusName = "В отпуске"}
                };
            context.EmployeeWorkingStatuses.AddRange(employeeWorkingStatuses);
            #endregion

            #region Добавление работников фитнес-клуба
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>
            {
               new Employee{
                   EmployeeFirstName = "Иванов",
                   EmployeeLastName = "Иван",
                   EmployeeFamilyName = "Иванович",
                   EmployeeDateOfBirdth = new DateTime(1989,05,24),
                   EmployeeAdress = "ул. Мальцева, д.5, кв.10",
                   EmployeePhoneNumber = "8-920-345-57-57",
                   EmployeeLoginName = "IvanovWorker",
                   EmployeePasswordHash="be50825e1851dfd342ddd6fce6cbd7fa",
                   EmployeeMail ="snHsd@inbox.ru",
                   EmployeeWorkingStatus =  employeeWorkingStatuses[(int)eWorkingStaus.eWorking],
                   EmployeeRole = roles[0]
               },
                new Employee{
                    EmployeeFirstName = "Петров",
                    EmployeeLastName = "Петр",
                    EmployeeFamilyName = "Петрович",
                    EmployeeDateOfBirdth = new DateTime(1980,01,21),
                    EmployeeAdress = "ул. Пушкина, д.15, кв.13",
                    EmployeePhoneNumber = "8-915-345-52-57",
                    EmployeeLoginName = "PetrovWorker",
                    EmployeePasswordHash="ace8eca0be0bead807aec0c2d18a77dc",
                    EmployeeMail ="ssdfsd@inbox.ru",
                    EmployeeWorkingStatus =  employeeWorkingStatuses[(int)eWorkingStaus.eSick],
                    EmployeeRole = roles[1]
                },
                new Employee{
                    EmployeeFirstName = "Сергеев",
                    EmployeeLastName = "Сергей",
                    EmployeeFamilyName = "Сергеевич",
                    EmployeeDateOfBirdth = new DateTime(1970,10,21),
                    EmployeeAdress = "ул. Пушкина, д.5, кв.1",
                    EmployeePhoneNumber = "8-915-435-52-51",
                    EmployeeLoginName = "SergeevWorker",
                    EmployeePasswordHash="ecb76c003e29175d00c24ea72334a91f",
                    EmployeeMail ="serg@mail.ru",
                    EmployeeWorkingStatus =   employeeWorkingStatuses[(int)eWorkingStaus.eVacation],
                    EmployeeRole = roles[2]
                },

            };
            context.Employees.AddRange(employees);
            #endregion

            #region Тарифы фитнес-клуба
            ObservableCollection<Tarif> tarifs = new ObservableCollection<Tarif>()
            {
                new Tarif{TarifName = "Утренний"},
                new Tarif{TarifName = "Вечерний"}
            };
            context.Tarifs.AddRange(tarifs);
            #endregion

            #region Добавление новой услуги
            ObservableCollection<Service> services = new ObservableCollection<Service>()
            {
                new Service {ServiceName = "Групповое занятие в фитнес-зале"},
                new Service {ServiceName = "Йога"},
                new Service {ServiceName = "Занятие в тренажерном зале"},
            };
            context.Services.AddRange(services);
            #endregion

            #region Добавление списка видов тренировок и услуг (прайс - лист)
            ObservableCollection<TrainingList> trainingsLists = new ObservableCollection<TrainingList>()
            {
                new TrainingList{ CountTrainingList = 1,TrainingListName=services[0],TrainingCurrentCost =200.0m, Tarifs = tarifs[0]},
                new TrainingList{ CountTrainingList = 2,TrainingListName=services[0],TrainingCurrentCost =400.0m, Tarifs = tarifs[0]},
                new TrainingList{ CountTrainingList = 3,TrainingListName=services[0],TrainingCurrentCost =600.0m, Tarifs = tarifs[1]},
            };
            context.TrainingLists.AddRange(trainingsLists);
            #endregion


            #region Добавление статусов тренировки
            ObservableCollection<StatusTraining> statusTrainingLists = new ObservableCollection<StatusTraining>()
            {
                new StatusTraining{StatusName = "Ведется запись"},
                new StatusTraining{StatusName = "Группа заполнена, запись невозможна"},
            };
            context.StatusTrainings.AddRange(statusTrainingLists);
            #endregion


            #region Добавление статусов аккаунта
            ObservableCollection<AccountStatus> accountStatusList = new ObservableCollection<AccountStatus>()
            {
                new AccountStatus{AccountStatusName = "Активен"},
                new AccountStatus{AccountStatusName = "Не активен"},
                new AccountStatus{AccountStatusName = "Заморожен"},
            };
            context.AccountStatuses.AddRange(accountStatusList);
            #endregion

            #region Добавление списка залов
            ObservableCollection<Gym> gymLists = new ObservableCollection<Gym>()
            {
                new Gym{GymName = "Зал N1",GymCapacity=15 },
                new Gym{GymName = "Зал N2",GymCapacity=10 },
                new Gym{GymName = "Зал N3",GymCapacity=20 },
            };
            context.Gyms.AddRange(gymLists);
            #endregion

            #region Добавление аккаунтов клиентов (посетителей фитнес-клуба)
            ObservableCollection<Account> accounts = new ObservableCollection<Account>
            {
                new Account{
                    NumberSubscription = AbonementGenerator.CreateNumberSubscription(context),
                    AccountregistrationDate = DateTime.Now,
                    TrainingCount = 10,
                    VisitedTrainingCount = 5,
                    TotalCost = 1000.0m, //сделать вычисляемым полем
                    ClientFirstName = "Антон",
                    ClientLastName = "Юрьевич",
                    ClientFamilyName = "Куликов",
                    ClientDateOfBirdth = DateTime.Parse("27.05.1989"),
                    ClientAdress = "г.Иваново, ул.Бакинский проезд, д.82, кв.11",
                    ClientPhoneNumber = "8-920-672-00-68",
                    ClientMail = "tosha37@inbox.ru",
                    /* ClientPhoto = ImageSQLController.ConvertToByteArray()*/ //реализовать
                    ClientPasportDataSeries = "2409",
                    ClientPasportDataNumber = "460870",
                    ClientPasportDataIssuedBy = "ОУФМС РОССИИ",
                    ClientPasportDatеOfIssue = new DateTime(2009,05,04),
                    Employee = employees[0],
                    ArrTrainingsList = new ObservableCollection<TrainingList>{ trainingsLists[0], trainingsLists[2] },
                    AccountStatus = accountStatusList[0] 
                },
                new Account{
                    NumberSubscription = AbonementGenerator.CreateNumberSubscription(context),
                    AccountregistrationDate = DateTime.Now,
                    TrainingCount = 15,
                    VisitedTrainingCount = 5,
                    TotalCost = 3000.0m, //сделать вычисляемым полем
                    ClientFirstName = "Николай",
                    ClientLastName = "Викторович",
                    ClientFamilyName = "Савлуков",
                    ClientDateOfBirdth = DateTime.Parse("27.05.1989"),
                    ClientAdress = "г.Иваново, ул.Бакинский проезд, д.82, кв.11",
                    ClientPhoneNumber = "8-920-672-00-68",
                    ClientMail = "tosha37@inbox.ru",
                    /* ClientPhoto = ImageSQLController.ConvertToByteArray()*/ //реализовать
                    ClientPasportDataSeries = "2409",
                    ClientPasportDataNumber = "460870",
                    ClientPasportDataIssuedBy = "ОУФМС РОССИИ",
                    ClientPasportDatеOfIssue = new DateTime(2009,05,04),
                    Employee = employees[0],
                    AccountStatus = accountStatusList[0]
                },
            };

            context.Accounts.AddRange(accounts);

            #endregion



            #region Добавление тренировки, которая предстоит
            ObservableCollection<Training> trainings = new ObservableCollection<Training>()
            {
                new Training
                {
                    TrainingDateTime = new DateTime(2018,05,24,15,30,00),
                    NumberOfSeats =10,
                    StatusTraining =statusTrainingLists[0],
                    ArrAccounts = new ObservableCollection<Account>{ accounts[0] , accounts[1] },
                    
                    Service = services[0],
                    Gym = gymLists[0],
                    Employee = employees[2]
                },

                new Training
                {
                    TrainingDateTime = new DateTime(2018,05,24,16,30,00),
                    NumberOfSeats =10,
                    StatusTraining =statusTrainingLists[0],
                    ArrAccounts = new ObservableCollection<Account>{ accounts[0] , accounts[1] },
                    Service = services[0],
                    Gym = gymLists[1],
                    Employee = employees[2]
                },
            };
            context.Trainings.AddRange(trainings);
            #endregion
            context.SaveChanges();
        }
    }
}
