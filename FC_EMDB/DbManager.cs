﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        /// Метод возвращает роль пользователя или eNone, если имя пользователя и пароль не совпадают с находящимися в БД
        /// </summary>
        /// <param name="strUserName">Имя пользователя</param>
        /// <param name="strPasswordHash">Хэш пароля</param>
        /// <param name="strRole">Роль пользователя системы</param>
        public void GetUserRole(string strUserName, string strPasswordHash, out string strRole)
        {
            var employee = unitOfWork.Employess.GetEmployeeByAutorizationUserData(strUserName, strPasswordHash);

            if (employee == null)
            {
                strRole = null;
                return;
            }

            strRole = unitOfWork.EmployeRoles.GetRole(employee).EmployeeRoleName;
        }

        /// <summary>
        /// Получить данные пользователя для отображения в окне информации
        /// </summary>
        /// <param name="datAutorizationUserData">Регистрационные данные пользователя</param>
        /// <param name="userData">Данные пользователя</param>
        public void GetSystemUserData(AutorizationUserData datAutorizationUserData, out UserData userData)
        {
            if (datAutorizationUserData == null)
            {
               userData = null;
               return;
            }

           var employee = unitOfWork.Employess.GetEmployeeByAutorizationUserData(datAutorizationUserData.UserLoginName, datAutorizationUserData.PasswordHash);

            // В отдельном потоке загрузим роль работника
            var outerNew = Task<EmployeeRole>.Factory.StartNew(() => unitOfWork.EmployeRoles.GetRole(employee));
            outerNew.Wait();
            var roleEmployee = outerNew.Result;

            //В отдельнгом потоке загрузим статус работника
            var outerNew1 = Task<string>.Factory.StartNew(() => unitOfWork.EmployeesWorkingStatus.GetEmployeeWorkingStatus(employee));
            outerNew1.Wait();
            var workingStatusEmployee = outerNew1.Result;

            if (employee == null)
            {
                userData = null;
                return;

            }

            userData = new UserData( employee.HumanFirstName, employee.HumanLastName, employee.HumanFamilyName,
             employee.HumanDateOfBirdth,  string.Empty , employee.HumanId, employee.HumanAdress, employee.HumanPhoneNumber, employee.HumanMail, employee.HumanPhoto, employee.EmployeeLoginName, employee.EmployeePasswordHash,
                roleEmployee, workingStatusEmployee);
            SqlTools.SavePhoto(ref userData);
        }

        /// <summary>
        /// Получить запись по заполненным данным
        /// </summary>
        /// <param name="recordData">Данные</param>
        public T GetRecord<T>( T recordData)  where  T : class 
        {
            if (recordData is Account )
            {

                return unitOfWork.Accounts.FindAccountWithSameData(recordData as Account) as T;
                //if (recordData != null)
                //{
                //    SqlTools.SavePhoto(ref recordData);
                //}
            }

            return null;

            //if (recordData is NewClientData)
            //{
            //    SqlTools.ConvertImageToByteArray( recordData);
            //    recordData = unitOfWork.Accounts.FindAccountWithSameData(recordData as NewClientData) as T;
            //    if (recordData != null)
            //    {
            //        SqlTools.SavePhoto(ref recordData);
            //    }
            //}
        }

        /// <summary>
        /// Обновить поля регистриации нового клиента
        /// </summary>
        /// <typeparam name="T">Шаблон</typeparam>
        /// <param name="recordData">Данные для записи</param>
        public void UpdateFields<T>(T recordData) where  T : class 
        {
            //if (recordData is Account)
            //{
            //    var _data = recordData as Account;

            //    if (_data.NumberSubscription != 0)// в случае, если это существующая запись
            //    {
            //        unitOfWork.Accounts.UpdateFields(_data);
            //    }
            //    else
            //    {
            //        unitOfWork.Accounts.CreateRecord(_data);
            //    }
              
            //}
        }

        /// <summary>
        /// Получить все зарегестрированные записи клментов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Зарегистриорванные записи клиентов</returns>
        public ObservableCollection<T> GetAllClients<T>() where T : class
        {
            return new ObservableCollection<T>(unitOfWork.Accounts.GetAll().Cast<T>()); 
        }

        /// <summary>
        /// Метод предоставляет спрачоные данны из бл
        /// </summary>
        /// <typeparam name="T">тип сущности</typeparam>
        /// <returns>коллекцию сущноситей конкретного типа</returns>
        public ObservableCollection<T> GetReferenceData<T>() where T : class 
        {
            if (typeof(T) == typeof(Tarif))
            {
                return new ObservableCollection<T>(unitOfWork.Tarif.GetAll().Cast<T>());
            }

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
            //if (data1  is Account && data2 is ServicesInSubscription)
            //{
            //    unitOfWork.Accounts.Get((data1 as Account).HumanId).ArrServicesInSubscription.Add( new ServicesInSubscription(data1 as Account)
            //        {
            //            SiSTrainingCount = (data2 as ServicesInSubscription).SiSTrainingCount,
            //            SiSVisitedTrainingCount = 0,
            //            TotalCost = (data2 as ServicesInSubscription).TotalCost,
            //            PriceType = (data2 as ServicesInSubscription).PriceType
            //    }    
            //    );
            //    unitOfWork.Complete();
            //}
        }

        public Account FindPersonForNumberSubsription(int numberSubscription) 
        {
            return unitOfWork.Accounts.GetAccountForNumberSubscription(numberSubscription);
        }
    }
}