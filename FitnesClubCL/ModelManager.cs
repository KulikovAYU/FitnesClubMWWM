using System;
using System.Collections.Generic;
using System.Drawing;
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
        /// <param name="strUserName">Имя пользователя</param>
        /// <param name="strPasswordString">Строка пароля</param>
        public void Autontefication(string strUserName, string strPasswordString, out string strRole)
        {
            if (strUserName == null && strPasswordString == null)
            {
                strRole = null;
                return;
            }
            _loginName = strUserName; //имя пользователя
            PasswordController.CheckUserNameAndPassword(LoginName, strPasswordString, out strRole);
        
           
            //При авторизации также получим данные пользователя
            PasswordController.GetSystemUserData(strUserName, strPasswordString, _userData);
            _loginName = _userData ["EmployeeLoginName"] as string;
            _userFullName = _userData["EmployeeFirstAndFamilyName"] as string;
            _status = _userData["EmployeeRoleName"] as string;
            _vacationStatus = _userData["EmployeeWorkingStatus"] as string;
            _dateOfBirdth = _userData["DateOfBirdth"] as DateTime?;
            _userPhoto = _userData["UserImage"] as Image;
            _timeInSystem = DateTime.Now;
            _userData.Clear();
        }


       #region Группа данных пользователя в системе
       private Image _userPhoto;
       private readonly Dictionary<string, object> _userData = new Dictionary<string, object>();
       private string _loginName;
       private string _userFullName;
       private string _status;
       private string _vacationStatus;
       private DateTime? _dateOfBirdth;
       private DateTime _timeInSystem;
        #endregion

       #region Группа данных, реализующих интерфейс ISystemUser
       public string UserFullName => _userFullName;
       public string LoginName => _loginName;
       public string Status => _status;
       public string VacationStatus => _vacationStatus;
       public Image UserPhoto => _userPhoto;
       public DateTime? DateOfBirdth => _dateOfBirdth;
       public DateTime TimeInSystem => _timeInSystem;
       #endregion




    }
}
