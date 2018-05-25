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
        public void CheckRecord<T>(ref T recordData) where T : class
        {
            if (recordData == null)
                return;
            ClientsHelper.IsExistRecord<T>(ref recordData);
        }

        public void UpdateRecord<T>(T clientData) where T : class
        {
            if (clientData == null)
                return;
            ClientsHelper.UpdateFields<T>(clientData);
        }
    }
}
