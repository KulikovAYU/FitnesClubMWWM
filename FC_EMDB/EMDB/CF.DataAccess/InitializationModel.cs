using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.DataAccess.Configuration;
using FC_EMDB.EMDB.CF.DataAccess.Context;
using FC_EMDB.Utils;

namespace FC_EMDB.EMDB.CF.DataAccess
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
        enum EWorkingStaus
        {
            EWorking, ESick, EVacation
        }

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
                   HumanFirstName = "Антон",
                   HumanLastName = "Юрьевич",
                   HumanFamilyName = "Куликов",
                   HumanDateOfBirdth = new DateTime(1989,05,24),
                   HumanAdress = "ул. Мальцева, д.5, кв.10",
                   HumanPhoneNumber = "8-920-345-57-57",
                   EmployeeLoginName = "IvanovWorker",
                   EmployeePasswordHash="be50825e1851dfd342ddd6fce6cbd7fa",
                   HumanPhoto = SqlTools.ConvertImageToByteArray(@"C:\Users\User\Pictures\Img\men.png"),
                   HumanMail ="snHsd@inbox.ru",
                   EmployeeWorkingStatus =  employeeWorkingStatuses[(int)EWorkingStaus.EWorking],
                   EmployeeRole = roles[0]
               },
                new Employee{
                    HumanFirstName = "Петров",
                    HumanLastName = "Петр",
                    HumanFamilyName = "Петрович",
                    HumanDateOfBirdth = new DateTime(1980,01,21),
                    HumanAdress = "ул. Пушкина, д.15, кв.13",
                    HumanPhoneNumber = "8-915-345-52-57",
                    EmployeeLoginName = "PetrovWorker",
                    EmployeePasswordHash="ace8eca0be0bead807aec0c2d18a77dc",
                    HumanMail ="ssdfsd@inbox.ru",
                    EmployeeWorkingStatus =  employeeWorkingStatuses[(int)EWorkingStaus.ESick],
                    EmployeeRole = roles[1]
                },
                new Employee{
                    HumanFirstName = "Сергеев",
                    HumanLastName = "Сергей",
                    HumanFamilyName = "Сергеевич",
                    HumanDateOfBirdth = new DateTime(1970,10,21),
                    HumanAdress = "ул. Пушкина, д.5, кв.1",
                    HumanPhoneNumber = "8-915-435-52-51",
                    EmployeeLoginName = "SergeevWorker",
                    EmployeePasswordHash="ecb76c003e29175d00c24ea72334a91f",
                    HumanMail ="serg@mail.ru",
                    EmployeeWorkingStatus =   employeeWorkingStatuses[(int)EWorkingStaus.EVacation],
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
            ObservableCollection<PriceTrainingList> trainingsLists = new ObservableCollection<PriceTrainingList>()
            {
                new PriceTrainingList{ TrainingListName=services[0],TrainingCurrentCost =200.0m, Tarifs = tarifs[0]},
                new PriceTrainingList{ TrainingListName=services[1],TrainingCurrentCost =400.0m, Tarifs = tarifs[0]},
                new PriceTrainingList{ TrainingListName=services[2],TrainingCurrentCost =600.0m, Tarifs = tarifs[1]},
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
            ObservableCollection<AbonementStatus> abonementStatusList = new ObservableCollection<AbonementStatus>()
            {
                new AbonementStatus {StatusName = "Активен"},
                new AbonementStatus{StatusName = "Не активен"},
                new AbonementStatus{StatusName = "Заморожен"},
            };
            context.AbonementStatuses.AddRange(abonementStatusList);
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

            ObservableCollection<Abonement> abonements = new ObservableCollection<Abonement>()
            {
                new Abonement()
                {
                    TrainingCount = 15,
                    VisitedTrainingCount = 2,
                    TotalCost = 4300.0m,
                    CountDays = DateTime.Parse("10.06.2018"),
                    ArrServicesInSubscription = new ObservableCollection<ServicesInSubscription>()
                    {
                        new ServicesInSubscription()
                        {
                            SiSTrainingCount = 7,
                            SiSVisitedTrainingCount = 4,
                            TotalCost = 1005.0m,
                            PriceType = trainingsLists[2]
                        },
                        new ServicesInSubscription()
                        {
                            SiSTrainingCount = 15,
                            SiSVisitedTrainingCount = 5,
                            TotalCost = 2000.0m,
                            PriceType = trainingsLists[0]
                        }
                    },

                    AbonmentStatus = abonementStatusList[0]
                },

                new Abonement()
                {
                    TrainingCount = 15, VisitedTrainingCount = 2, TotalCost = 3000.0m, CountDays = DateTime.Parse("10.06.2018"),
                    ArrServicesInSubscription = new ObservableCollection<ServicesInSubscription>()
                    {
                        new ServicesInSubscription(){ SiSTrainingCount = 7, SiSVisitedTrainingCount=4, TotalCost = 1005.0m, PriceType =trainingsLists[2] },
                        new ServicesInSubscription(){ SiSTrainingCount = 15, SiSVisitedTrainingCount=5, TotalCost = 2000.0m, PriceType =trainingsLists[0] }
                    },
                    AbonmentStatus = abonementStatusList[1]
                },

            };
          

            #region Добавление аккаунтов клиентов (посетителей фитнес-клуба)
            ObservableCollection<Account> accounts = new ObservableCollection<Account>
            {
                new Account{
                    Abonement = abonements[0],
                    //NumberSubscription = AbonementGenerator.CreateNumberSubscription(),
                    //AccountregistrationDate = DateTime.Now,
                    //TrainingCount = 10,
                    //VisitedTrainingCount = 5,
                    //TotalCost = 1000.0m, //сделать вычисляемым полем
                    HumanFirstName = "Антон",
                    HumanLastName = "Юрьевич",
                    HumanFamilyName = "Куликов",
                    HumanGender="Муж.",
                    HumanDateOfBirdth = DateTime.Parse("27.05.1989"),
                    HumanAdress = "г.Иваново, ул.Бакинский проезд, д.82, кв.11",
                    HumanPhoneNumber = "8-920-672-00-68",
                    HumanMail = "tosha37@inbox.ru",
                    HumanPhoto = SqlTools.ConvertImageToByteArray(@"C:\Users\User\Pictures\Img\men.png"),
                    HumanPasportDataSeries = "2409",
                    HumanPasportDataNumber = "460870",
                    HumanPasportDataIssuedBy = "ОУФМС РОССИИ",
                    HumanPasportDatеOfIssue =  DateTime.Parse("04.05.2009"),
                    //Employee = employees[0],
                    //TypeAbonement = trainingsLists[0],
                    //AccountStatus = accountStatusList[0],
                    //CountDays = 30,
                    //AbonementActivationDateTime = DateTime.Parse("24.03.2018"),
                   
                },
                new Account{
                    Abonement = abonements[1],
                    //NumberSubscription = AbonementGenerator.CreateNumberSubscription(),
                    //AccountregistrationDate = DateTime.Now,
                    //TrainingCount = 15,
                    //VisitedTrainingCount = 5,
                    //TotalCost = 3000.0m, //сделать вычисляемым полем
                    HumanFirstName = "Анастасия",
                    HumanLastName = "Николаевна",
                    HumanFamilyName = "Смирнова",
                    HumanGender="Жен.",
                    HumanDateOfBirdth = DateTime.Parse("24.03.2000"),
                    HumanAdress = "г.Иваново, ул.Бакинский проезд, д.82, кв.11",
                    HumanPhoneNumber = "8-920-672-00-68",
                    HumanMail = "tosha37@inbox.ru",
                    HumanPhoto = SqlTools.ConvertImageToByteArray(@"C:\Users\User\Pictures\Img\girl.jpg"),
                    HumanPasportDataSeries = "2409",
                    HumanPasportDataNumber = "460870",
                    HumanPasportDataIssuedBy = "ОУФМС РОССИИ",
                    HumanPasportDatеOfIssue =  DateTime.Parse("04.05.2012"),
                    //Employee = employees[0],
                    //TypeAbonement = trainingsLists[2],
                    //AccountStatus = accountStatusList[1],
                    //CountDays = 25,
                    //AbonementActivationDateTime = DateTime.Parse("20.01.2018"),
                    //ArrServicesInSubscription = new ObservableCollection<ServicesInSubscription>()
                    //{
                    //    new ServicesInSubscription(){ SiSTrainingCount = 3, SiSVisitedTrainingCount=3, TotalCost = 1200.0m, arrPriceType =trainingsLists[1] },
                    //    new ServicesInSubscription(){ SiSTrainingCount = 8, SiSVisitedTrainingCount=5, TotalCost = 2040.0m, arrPriceType =trainingsLists[2] }
                    //}
                },
            };

          
            #endregion

            //ObservableCollection<Abonement> _Abonements = new ObservableCollection<Abonement>();
            //context.Abonements.AddRange(_Abonements);

            #region Добавление тренировки, которая предстоит
            ObservableCollection<UpcomingTraining> trainings = new ObservableCollection<UpcomingTraining>()
            {
                new UpcomingTraining
                {
                    TrainingDateTime = new DateTime(2018,05,24,15,30,00),
                    NumberOfSeats =10,
                    StatusTraining =statusTrainingLists[0],
                    Abonements = new ObservableCollection<Abonement>{ accounts[0].Abonement , accounts[1].Abonement },
                    Service = services[0],
                    Gym = gymLists[0],
                    Employee = employees[2]
                },

                new UpcomingTraining
                {
                    TrainingDateTime = new DateTime(2018,05,24,16,30,00),
                    NumberOfSeats =12,
                    StatusTraining =statusTrainingLists[0],
                    Abonements = new ObservableCollection<Abonement>{ accounts[0].Abonement , accounts[1].Abonement },
                    Service = services[0],
                    Gym = gymLists[1],
                    Employee = employees[2]
                },
            };
            context.Trainings.AddRange(trainings);

            ObservableCollection<ServicesInSubscription> _collectionSiS = new ObservableCollection<ServicesInSubscription>()
            {
                new ServicesInSubscription() { SiSTrainingCount = 3, SiSVisitedTrainingCount=1, TotalCost=254.0m, PriceType = trainingsLists[0]},
                new ServicesInSubscription() { SiSTrainingCount = 2, SiSVisitedTrainingCount=0, TotalCost=204.0m, PriceType = trainingsLists[1]}
            };
           


            abonements[0].Account = accounts[0];
            abonements[1].Account = accounts[1];

            accounts[0].Abonement.ArrServicesInSubscription.Add(_collectionSiS[0]);
            accounts[0].Abonement.ArrServicesInSubscription.Add(_collectionSiS[1]);
          
            accounts[1].Abonement.ArrServicesInSubscription.Add(_collectionSiS[0]);
            accounts[1].Abonement.ArrServicesInSubscription.Add(_collectionSiS[1]);
            context.Abonements.AddRange(abonements);

            context.Accounts.AddRange(accounts);
            context.ServicesInSubscription.AddRange(_collectionSiS);

            #endregion
           context.SaveChanges();
        }
    }
}
