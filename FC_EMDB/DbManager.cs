using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using FC_EMDB.Classes;
using FC_EMDB.Constants;
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


           userData = new UserData(employee.EmployeeId, employee.EmployeeFirstName, employee.EmployeeLastName, employee.EmployeeFamilyName,
             employee.EmployeeDateOfBirdth,   employee.EmployeeAdress, employee.EmployeeLoginName, employee.EmployeePasswordHash,employee.EmployeeMail,
               employee.EmployeePhoneNumber, roleEmployee, workingStatusEmployee, employee.EmployeePhoto);
        }

        public void RegisterNewClient(Dictionary<string, object> clientData)
        {

            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Метод проверяет данные клиента, а также заполнение обязательных полей и в случае, если данные клиента совпадают с БД, то возвращает успех, а также новый массив с данними о клиенте
        /// </summary>
        /// <param name="arrclientData"> данные клиента</param>
        public bool IsExistClient(Dictionary<string, object> arrclientData)
        {
           Account acc = new Account()
           {
               ClientFirstName = arrclientData["ClientFirstName"] as string,
               ClientLastName = arrclientData["ClientLastName"] as string,
               ClientFamilyName = arrclientData["ClientFamilyName"] as string,
               ClientDateOfBirdth = arrclientData["ClientDateOfBirdth"] as DateTime?,
               ClientPhoneNumber = arrclientData["ClientPhoneNumber"] as string,
               ClientPasportDataSeries = arrclientData["ClientPasportDataSeries"] as string,
               ClientPasportDataNumber = arrclientData["ClientPasportDataNumber"] as string,
               ClientPasportDataIssuedBy = arrclientData["ClientPasportDataIssuedBy"] as string,
               ClientPasportDatеOfIssue = arrclientData["ClientPasportDatеOfIssue"] as DateTime?
           };
            arrclientData.Clear();
            Account newAcc =   unitOfWork.Accounts.FindAccountWithSameData(acc);

            if (newAcc != null)
            {
                //Соберем найденные данные в случае успеха и вернем в массив arrclientData
                arrclientData["ClientFirstName"] = newAcc.ClientFirstName;
                arrclientData["ClientLastName"] = newAcc.ClientLastName;
                arrclientData["ClientFamilyName"] = newAcc.ClientFamilyName;
                arrclientData["ClientDateOfBirdth"] = newAcc.ClientDateOfBirdth;
                arrclientData["ClientPhoneNumber"] = newAcc.ClientPhoneNumber;
                arrclientData["ClientPasportDataSeries"] = newAcc.ClientPasportDataSeries;
                arrclientData["ClientPasportDataNumber"] = newAcc.ClientPasportDataNumber;
                arrclientData["ClientPasportDataIssuedBy"] = newAcc.ClientPasportDataIssuedBy;
                arrclientData["ClientPasportDatеOfIssue"] = newAcc.ClientPasportDatеOfIssue;
                arrclientData["ClientEmail"] = newAcc.ClientMail;
                arrclientData["ClientPhoto"] = Image.FromStream(new MemoryStream(newAcc.ClientPhoto));
            }
           return newAcc != null;
        }
    }
}