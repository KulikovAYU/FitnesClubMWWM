using System.Collections.Generic;
using FC_EMDB;
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

       public static ModelManager GetInstance()
       {
          return _mManager ?? (_mManager = new ModelManager());
       }

       public void CreateDB()
       {
           DbManager.GetInstance().InitializeDatabase();
       }

        /// <summary>
        /// Аутентификация пользоватля в системе
        /// </summary>
        /// <param name="datasAutorizationUserData">Данные пользователя для аутентификации</param>
        public void Autontefication(AutorizationUserData datasAutorizationUserData)
        {
            if (datasAutorizationUserData.UserLoginName == null || datasAutorizationUserData.PasswordString == null)
                return;

            PasswordController.CheckUserNameAndPassword(datasAutorizationUserData);
            //При авторизации также получим данные пользователя
            Dictionary<string, object> arrUserData = new Dictionary<string, object>();
            PasswordController.GetSystemUserData(datasAutorizationUserData,arrUserData);
            WorkingUserData = arrUserData.Count == 0 ? (UserData?) null : new UserData(arrUserData);
        }

       public AutorizationUserData AutorizationUserData { get; }
       public UserData? WorkingUserData { get; private set; }
    }
}
