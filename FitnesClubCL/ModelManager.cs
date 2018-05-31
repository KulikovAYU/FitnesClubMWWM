using System;
using System.Collections.ObjectModel;
using FC_EMDB;
using FC_EMDB.Classes;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnesClubCL.Annotations;
using FitnesClubCL.Classes;
using GalaSoft.MvvmLight;

namespace FitnesClubCL
{
    /// <summary>
    /// Класс представляет интерфейс для работы с моделью данных
    /// </summary>
    public sealed class ModelManager : ViewModelBase, ISystemUser
    {
    
       private ModelManager()
       {
         
        }

       private static ModelManager _mManager;

        public static ModelManager GetInstance => _mManager ?? (_mManager = new ModelManager());

        public void CreateDB()
       {
           DbManager.GetInstance().InitializeDatabase();
       }


        #region Методы и поля, связанные с пользователями информационной системы 
        /// <summary>
        /// Аутентификация пользоватля в системе
        /// </summary>
        /// <param name="datAutorizationUserData">Данные пользователя для аутентификации</param>
        public void Autontefication([CanBeNull] AutorizationUserData datAutorizationUserData)
        {
            if (string.IsNullOrEmpty(datAutorizationUserData?.UserLoginName) ||
                string.IsNullOrEmpty(datAutorizationUserData.PasswordString))
            {
                return;
            }

            AutorizationUserData = datAutorizationUserData;
            //При авторизации также получим данные пользователя
            PasswordController.GetSystemUserData(AutorizationUserData, out UserData userData);
            //Вернем данные пользователя
            WorkingUserData = userData;
        }

        /// <summary>
        /// Указатель на данные пользователя
        /// </summary>
        [CanBeNull] public AutorizationUserData AutorizationUserData { get; private set; }

        /// <summary>
        /// Экземпляр данных работника
        /// </summary>
        [CanBeNull] public UserData WorkingUserData { get; private set; }
        #endregion


    

        /// <summary>
        /// Проверить запись на существование
        /// </summary>
        /// <typeparam name="T">Шаблон параметра</typeparam>
        /// <param name="recordData">данные записи</param>
        public T CheckRecord<T>(T recordData) where T : class
        {
            if (recordData == null)
                return null;
            return ClientsHelper.IsExistRecord<T>(recordData);
        }

        /// <summary>
        /// Метод обновляет запись либо создает новую запись в БД
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientData"></param>
        public void CreateOrUpdateRecord<T>(T clientData) where T : class
        {
            if (clientData == null)
                return;
            
            ClientsHelper.UpdateFields<T>(clientData);
        }


        public Account FindPersonForNumberSubsription(int numberSubscription) 
        {
            if (string.IsNullOrEmpty(numberSubscription.ToString()) )
            {
                return null;
            }

           return ClientsHelper.FindPersonForNumberSubsription(numberSubscription);
        }


        /// <summary>
        /// Метод возвращает коллекцию людей (работников, клиентов...)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>коллекция данных</returns>
        public static ObservableCollection<T> GetAllPersons<T>() where T : class
        {
            if (typeof(T) == typeof(Account)) //если запросили коллекцию работников
            {
              return  ClientsHelper.GetAllClients<T>();
            }

            return null;
        }


        /// <summary>
        /// Метод предоставляет спрачоные данны из бд
        /// </summary>
        /// <typeparam name="Template">тип сущности</typeparam>
        /// <returns>коллекцию сущноситей конкретного типа</returns>
        public static ObservableCollection<Template> GetReferenceData<Template>() where Template : class 
        {
            return Assistiant.GetReferenceData<Template>();
        }

        public static void SaveData<T>(T data) where T : class
        {
            Assistiant.SaveData<T>(data);
        }

        public static void AddData<T1, T2>(T1 data1, T2 data2) where T1 : class where T2 : class
        {
            Assistiant.AddData<T1, T2>(data1, data2);
        }

       
    }
}
