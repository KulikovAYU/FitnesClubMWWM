using FitnesClubCL.CF_EMDB;
using FitnesClubCL.Utils;
using GalaSoft.MvvmLight;

namespace FitnesClubCL
{
   public class ModelManager : ViewModelBase
    {
        #region Конструктора

        static ModelManager()
        {
            if (_dataBaseContext == null)
                _dataBaseContext = new DataBaseFcContext("dbContext");
        }


        protected ModelManager()
       {

       }

        #endregion

        #region Поля класса
        private static ModelManager _manager;
        private static DataBaseFcContext _dataBaseContext ;
        #endregion

        public static ModelManager GetInstance()
        {
            return _manager ?? (_manager = new ModelManager());
        }

        public static DataBaseFcContext GetDbContext()
        {
            return _dataBaseContext ?? (_dataBaseContext = new DataBaseFcContext("dbContext"));
        }

        /// <summary>
        /// Метод создает БД
        /// </summary>
        public void CreateDb()
        {
           
            using (/*_dataBaseContext*/ var unitOfWork = new UnitOfWork(_dataBaseContext))
            {
                //TODO: Внимание, для того, чтобы сработала дефолтная инициализация, необходимо прописать тут хоть одну инициализацию

                #region Тарифы фитнес-клуба
                //var tarifs = new Tarif(){ TarifName = "Студенческий"};
                //_dataBaseContext.Tarifs.Add(tarifs);
                //_dataBaseContext.SaveChanges();
                #endregion
                // SQLTools.ConvertToImageFromByteArray(eEntities.eClient, 1); // TODO: отладка для проверки конвертации изображения из БД
            }
        }

        

    
    }
}
