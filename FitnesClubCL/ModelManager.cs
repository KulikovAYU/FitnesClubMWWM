using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FitnesClubCL.Annotations;
using FitnesClubCL.CF_EMDB;
using FitnesClubCL.Utils;

namespace FitnesClubCL
{
   public class ModelManager : INotifyPropertyChanged
    {
        #region Конструктора

       protected ModelManager()
       {

       }

        #endregion

        #region Поля класса
        private static ModelManager Manager;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public static ModelManager GetInstance()
        {
            return Manager ?? (Manager = new ModelManager());
        }

      
       // delegate int MyDelegate(DataBaseFcContext context);
        /// <summary>
        /// Метод создает БД
        /// </summary>
        public void CreateDb()
        {
            using (var db = new DataBaseFcContext("dbContext"))
            {
                //TODO: Внимание, для того, чтобы сработала дефолтная инициализация, необходимо прописать тут хоть одну инициализацию
                //анонимный метод, генерирующий абонемент

                #region Тарифы фитнес-клуба
                var tarifs = new Tarif(){ TarifName = "Студенческий"};
                db.Tarifs.Add(tarifs);
                db.SaveChanges();
                #endregion


            }
        }

        

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
