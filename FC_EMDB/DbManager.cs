using FC_EMDB.Constants;
using FC_EMDB.EMDB.CF.DataAccess;
using FC_EMDB.EMDB.CF.DataAccess.Context;


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
        /// <param name="role">Роль пользователя системы</param>
        public void GetUserRole(string strUserName, string strPasswordHash, out eRoles role)
        {
            var employee = unitOfWork.Employess.GetEmployeeByUserNameAndPassword(strUserName, strPasswordHash);

            if (employee == null)
            {
                role = eRoles.eNone;
                return;
            }

            string strRole = unitOfWork.EmployeRoles.GetRole(employee).EmployeeRoleName;
            switch (strRole)
            {
                case ("Администратор"):
                    role = eRoles.eAdministrator;
                    break;

                case ("Фитнес-инструктор"):
                    role = eRoles.eFitnesInstructor;
                    break;

                case ("Менеджер"):
                    role = eRoles.eManager;
                    break;

                case ("Руководитель"):
                    role = eRoles.eDirector;
                    break;

                default:
                    role = eRoles.eNone;
                    break;
            }
        }
    }
}