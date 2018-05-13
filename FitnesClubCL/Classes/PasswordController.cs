using FC_EMDB;
using FC_EMDB.Constants;

namespace FitnesClubCL.Classes
{
    /// <summary>
    /// класс для управления паролями пользователей
    /// </summary>
public static class PasswordController
    {
        /// <summary>
        /// Метод проверяет имя пользователя и пароль
        /// </summary>
       public static void CheckUserNameAndPassword(string strUserName, string strPasswordHash, out eRoles role)
        {
            DbManager.GetInstance().GetUserRole(strUserName, strPasswordHash, out role);
        }
    }
}
