
using FC_EMDB;
using FC_EMDB.Constants;
using FitnesClubCL.Classes;
using GalaSoft.MvvmLight;

namespace FitnesClubCL
{
    /// <summary>
    /// Класс представляет интерфейс для работы с моделью данных
    /// </summary>
   public sealed class ModelManager : ViewModelBase
   {
    
       private ModelManager()
       {
       }

       private static ModelManager m_manager;

       public static ModelManager GetInstance()
       {
          return m_manager ?? (m_manager = new ModelManager());
       }

       public void CreateDB()
       {
           DbManager.GetInstance().InitializeDatabase();
       }

        /// <summary>
        /// Аутентификация пользователя в системе
        /// </summary>
       public void Autontefication(string strUserName, string strPasswordHash, out eRoles role)
       {
           PasswordController.CheckUserNameAndPassword(strUserName, strPasswordHash, out role);
       }

   }
}
