using System.Collections.Generic;
using FC_EMDB;
using FC_EMDB.Classes;
using FitnesClubCL.Annotations;
using FitnesClubCL.Classes;
using GalaSoft.MvvmLight;

namespace FitnesClubCL
{
    /// <summary>
    /// Класс представляет интерфейс для работы с моделью данных
    /// </summary>
    public sealed class ModelManager : ViewModelBase, ISystemUser, IClientRegisterData
    {
    
       private ModelManager()
       {
         
        }

       private static ModelManager _mManager;

       public static ModelManager GetInstance()
       {
          return _mManager ?? (_mManager = new ModelManager());
       }

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









        public NewClientData ClientData { get; set; }

        /// <summary>
        /// Зарегистрировать нового клиента
        /// </summary>
        /// <param name="clientData">данные клиента</param>
        public void RegisterNewClient(ref NewClientData clientData)
        {
           
          

        }

      
    }
}
