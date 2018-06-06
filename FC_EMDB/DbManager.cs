using System;
using System.Collections.ObjectModel;
using System.Linq;
using FC_EMDB.Classes;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.DataAccess;
using FC_EMDB.EMDB.CF.DataAccess.Context;
using FC_EMDB.Utils;


namespace FC_EMDB
{
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    public sealed class DbManager
    {
        #region Конструктора

        static DbManager()
        {
            if (_dataBaseContext == null)
                _dataBaseContext = new DataBaseFcContext("dbContext");
            if (_manager == null)
                _manager = new DbManager();
           
        }


        private DbManager()
        {

        }

        #endregion

        #region Поля класса
        private static readonly DbManager _manager;
        private static readonly DataBaseFcContext _dataBaseContext;
       

        #endregion

        public static DbManager GetInstance()
        {
            return _manager ?? new DbManager();
        }

        public static DataBaseFcContext GetDbContext()
        {
            return _dataBaseContext ?? new DataBaseFcContext("dbContext");
        }


        private static readonly UnitOfWork unitOfWork = new UnitOfWork(GetDbContext());

        /// <summary>
        /// Метод создает БД
        /// </summary>
        public void InitializeDatabase()
        {

            //using (/*_dataBaseContext*/  unitOfWork = new UnitOfWork(_dataBaseContext))
            //{
            //    //TODO: Внимание, для того, чтобы сработала дефолтная инициализация, необходимо прописать тут хоть одну инициализацию

            //    #region Тарифы фитнес-клуба

            //    //var tarifs = new Tarif(){ TarifName = "Студенческий"};
            //    //_dataBaseContext.Tarifs.Add(tarifs);
            //    //_dataBaseContext.SaveChanges();
            //    #endregion
            //    // SQLTools.ConvertToImageFromByteArray(eEntities.eClient, 1); // TODO: отладка для проверки конвертации изображения из БД
            //}
        }

        /// <summary>
        /// Получить данные пользователя для отображения в окне информации
        /// </summary>
        /// <param name="datAutorizationUserData">Регистрационные данные пользователя</param>
        /// <param name="userData">Данные пользователя</param>
        public void GetSystemUserData(AutorizationUserData datAutorizationUserData, out Employee userData)
        {
            if (datAutorizationUserData == null)
            {
               userData = null;
               return;
            }

           var employee = unitOfWork.Employess.GetEmployeeByAutorizationUserData(datAutorizationUserData.UserLoginName, datAutorizationUserData.PasswordHash);
 
            if (employee == null)
            {
                userData = null;
                return;
            }

            SqlTools.SavePhoto(ref employee);
            userData = employee;
        }

        /// <summary>
        /// Получить запись по заполненным данным
        /// </summary>
        /// <param name="recordData">Данные</param>
        public T GetRecord<T>( T recordData)  where  T : class 
        {
            return  unitOfWork.Accounts.FindAccountWithSameData(recordData as Account) as T;
        }

        /// <summary>
        /// Обновить поля регистриации нового клиента
        /// </summary>
        /// <typeparam name="T">Шаблон</typeparam>
        /// <param name="recordData">Данные для записи</param>
        public void UpdateFields<T>(T recordData) where  T : class 
        {
            if (recordData is Account)
            {
                var _data = recordData as Account;

                if (_data.Abonement != null && _data.Abonement.NumberSubscription != 0)// в случае, если это существующая запись
                {
                    unitOfWork.Accounts.UpdateFields(_data);
                }
                else
                {
                    unitOfWork.Accounts.CreateRecord(_data);
                }
            }

            if (recordData is Gym)
            {
                var _data = recordData as Gym;
                unitOfWork.Gyms.CreateOrUpdateRecord(_data);
            }
        }

        /// <summary>
        /// Получить все зарегестрированные записи клментов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Зарегистриорванные записи клиентов</returns>
        public ObservableCollection<T> GetAllClients<T>() where T : class
        {
            var res = new ObservableCollection<T>(unitOfWork.Accounts.GetAll().Cast<T>());
            //foreach (var acc in res)
            //{
            //    var acc1 = acc;
            //    SqlTools.SavePhoto(ref acc1);
            //}

            return res;
        }

        /// <summary>
        /// Метод предоставляет спрачоные данны из бл
        /// </summary>
        /// <typeparam name="T">тип сущности</typeparam>
        /// <returns>коллекцию сущноситей конкретного типа</returns>
        public ObservableCollection<T> GetReferenceData<T>() where T : class 
        {


            if (typeof(T) == typeof(Service))
            {
                return new ObservableCollection<T>(unitOfWork.Services.GetAll().Cast<T>());
            }

            if (typeof(T) == typeof(PriceTrainingList))
            {
                return new ObservableCollection<T>(unitOfWork.TrainingList.GetAll().Cast<T>());
            }

            if (typeof(T) == typeof(AccountStatus))
            {
                return new ObservableCollection<T>(unitOfWork.AccountStatus.GetAll().Cast<T>());
            }

            if (typeof(T) == typeof(Gym))
            {
                return new ObservableCollection<T>(unitOfWork.Gyms.GetAll().Cast<T>());
            }

            return null;

        }

        /// <summary>
        /// Сохранить данные
        /// </summary>
        /// <typeparam name="T">тип параметра</typeparam>
        public void SaveData<T>(T data) where T : class
        {
            if (data is Account)
            {
                unitOfWork.Accounts.AppentRecordToExistAccount(data as Account);
            }
        }

        public void AddData<T1, T2>(T1 data1, T2 data2) where T1 : class where T2 : class
        {
            if (data1 is Abonement && data2 is ServicesInSubscription)
            {
                var currentAccount = unitOfWork.Abonements.Get((data1 as Abonement).AbonementId);

                var currentTraining = currentAccount.ArrServicesInSubscription.FirstOrDefault(service =>
                    service.PriceType.TrainingListName.ServiceName ==
                    (data2 as ServicesInSubscription)?.PriceType.TrainingListName.ServiceName);

                if (currentTraining != null) //если тренировки нет в списке
                {
                    currentTraining.SiSTrainingCount += (data2 as ServicesInSubscription).SiSTrainingCount;
                }
                else//если тренировка есть в списке
                {
                    (data1 as Abonement).ArrServicesInSubscription.Add(new ServicesInSubscription()
                        {
                            PriceType = (data2 as ServicesInSubscription).PriceType,
                            SiSTrainingCount = (data2 as ServicesInSubscription).SiSTrainingCount,
                            SiSTrainingName = (data2 as ServicesInSubscription).SiSTrainingName,
                            TotalCost = (data2 as ServicesInSubscription).TotalCost,
                        }
                    );
                    (data1 as Abonement).CountDays = DateTime.Now.AddMonths(1);//активираем абонемент
                }
            }

            //сейвим изменения
            unitOfWork.Complete();
        }

        public Account FindPersonForNumberSubsription(int numberSubscription)
        {
            return unitOfWork.Abonements.GetAccountForNumberSubscription(numberSubscription);
           
            //return unitOfWork.Accounts.GetAccountForNumberSubscription(numberSubscription);
        }

        /// <summary>
        /// Получить дотупные для пользователя расписание занятий по конкретной тренировке
        /// </summary>
        /// <param name="servicesInSubscription">доступные услуги</param>
        public ObservableCollection<UpcomingTraining> GetAvailableTrainings(ServicesInSubscription servicesInSubscription)
        {

            return unitOfWork.Services.GetAvailableTrainings(servicesInSubscription);

        }

        /// <summary>
        /// Проверка тренировки по факту доступности (число мест !=0)
        /// </summary>
        /// <param name="account">аккаунт</param>
        /// <param name="item">предстоящая тренировка</param>
        /// <returns></returns>
        public bool IsRecordAvailable(Account account, UpcomingTraining item)
        {
            if (item == null || account == null) return false;

            bool bIsNotContainsTraining = !account.Abonement.ArrUpcomingTrainings.Contains(item);
            return unitOfWork.Services.CheckTrainingOnAvailable(item) && bIsNotContainsTraining;
        }

        /// <summary>
        /// Создать предварительную регистрацию тренировки
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <param name="currentItem">Выбранная тренировка</param>
        public void CreatePriorRegistration(Account account, ServicesInSubscription service, UpcomingTraining currentItem)
        {
            var servicesInSubscriptions = account.Abonement.ArrServicesInSubscription.Where(serv =>
                serv.PriceType.TrainingListName.ServiceName == currentItem.Service.ServiceName).FirstOrDefault();
            if (servicesInSubscriptions.SiSTrainingCount == 1)
            {
                //сохраним ссылку на тренировку
                currentItem.CurrentService = new ServicesInSubscription(service);
            }

         
            //1. Получим аккаунт
            account.Abonement.ArrUpcomingTrainings.Add(currentItem);
            service.SiSTrainingCount--;
            if (service.SiSTrainingCount == 0)
            {
                account.Abonement.ArrServicesInSubscription.Remove(service);
            }
            //// //1. Получили тренировку
            ////var currentService = unitOfWork.ServicesInSubscription.Get(service.SiSId);
            ////2. Уменьшили счетчик
            // currentService.SiSTrainingCount--;
            //3. Засейвили изменения
            //  unitOfWork.ServicesInSubscription.AddOrUpdate(currentService);

            unitOfWork.Complete();
            //Также необходимо создать новую запись
           // unitOfWork.UpcomingTrainings.AddNewUpcomingTraining(account, currentItem);
        }

        /// <summary>
        /// Звафиксировыать посещение тренировки
        /// </summary>
        /// <param name="account">Учетная запись</param>
        /// <param name="upcomingTraining">Предстоящая тренировка</param>
        public void FixTheVisit(Account account, UpcomingTraining upcomingTraining)
        {
            if (account == null || upcomingTraining == null)
            {
                return;
            }
            unitOfWork.UpcomingTrainings.FixTheVisit(account, upcomingTraining);
        }

        /// <summary>
        /// Отказ о посещении тренировки
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <param name="upcTraining">предстоящая тренировка</param>
        public void RefusalOfVisit(Account account, UpcomingTraining upcTraining)
        {
            if (account == null || upcTraining == null)
            {
                return;
            }

            //1. Получили предстоящую тренировку
           var upcomTraining = unitOfWork.UpcomingTrainings.GetUpcomingTraining(upcTraining);
            //2. получили текущий аккаунт
            var currAccount = unitOfWork.Accounts.FindAccountWithSameData(account);
            //3. Получили отмененную тренировку
            var CancelledTraining =  currAccount.Abonement.ArrServicesInSubscription.FirstOrDefault(upcTr=>upcTr.PriceType.TrainingListName.ServiceName ==
                                                                                         upcTraining.Service.ServiceName);

            if (CancelledTraining == null)
            {
                currAccount.Abonement.ArrServicesInSubscription.Add(new ServicesInSubscription()
                {
                    SiSTrainingName = upcTraining.Service.ServiceName,
                    SiSTrainingCount = 1,
                    PriceType = upcTraining.CurrentService.PriceType
                });
            }
            else
            {
                CancelledTraining.SiSTrainingCount++;
              //  //Нашли текущую тренировку
              //var existTraining =  currAccount.Abonement.ArrServicesInSubscription.FirstOrDefault(upcTr =>
              //      upcTr.SiSTrainingName == upcTraining.Service.ServiceName);
              //  if (existTraining != null)
              //  {
              //      existTraining.SiSTrainingCount++;
              //  }
            
                ////4. Увеличим счетчик текущей тренировки
                //unitOfWork.ServicesInSubscription.IncrementCountServices(upcTraining);
                
            }
            //3. Убрали тренировку из записи
            currAccount.Abonement.ArrUpcomingTrainings.Remove(upcTraining);
            ////3. Убрали тренировку из записи
            //unitOfWork.UpcomingTrainings.Remove(upcTraining);
            //Сохраним изменения в БД
            unitOfWork.Complete();
        }

        /// <summary>
        /// Получить цены и названия тренировок
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<PriceTrainingList> GetPriceTrainingList()
        {
           return new ObservableCollection<PriceTrainingList>(unitOfWork.PriceTrainingLists.GetAll());
        }


        public void SetTotalCost(Abonement accountAbonement)
        {
           unitOfWork.ServicesInSubscription.SetTotalCost(accountAbonement);
        }

        public void RemoveItem<T>(T item) where T : class
        {
            if (item is Gym)
            {
               var  currentGym = unitOfWork.Gyms.Get((item as Gym).GymId);
                unitOfWork.UpcomingTrainings.GetTrainingWithCurrentGym((item as Gym));

               
                unitOfWork.Gyms.Remove(currentGym);
                
                unitOfWork.Complete();
            }
        }
    }
}