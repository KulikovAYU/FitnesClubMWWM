/// <summary>
/// Данный файл описывает структуру, которая передается вью модели
/// </summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FC_EMDB.EMDB.CF.Data.Domain;
using GalaSoft.MvvmLight.Messaging;

namespace FitnesClubCL.Classes
{
    /// <summary>
    /// Структура "Авторизационный данные пользователя"
    /// </summary>
    public struct AutorizationUserData
    {
        public AutorizationUserData(string userLoginName, string passwordString)
        {
            this.UserLoginName = userLoginName;
            this.PasswordString = passwordString;
            PasswordHash = PasswordController.GeneratePasswordHash(PasswordString);
        }
 
        public string UserLoginName { get; private set; }
        public string PasswordString { get; private set; }
        public string PasswordHash{get;private set;}
    }

    /// <summary>
    /// Структура: данные пользователя после авторизации
    /// </summary>
    public struct UserData 
    {
        static UserData()
        {

        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userData">данные пользователя</param>
        public UserData(Dictionary<string, object> userData)
        {
            if (userData.Count == 0)
            {
                this.userData = null;
                UserLoginName = null;
                UserFullName = null;
                UserStatus = null;
                UserVacationStatus = null;
                UserDateOfBirdth = null;
                UserPhoto = null;
                Path = null;
                GetAcsessRigths = new AcsessRigths();
                return;
            }
            this.userData = userData;
            GetAcsessRigths = null;
            UserLoginName = userData["EmployeeLoginName"] as string;
            UserFullName = userData["EmployeeFirstAndFamilyName"] as string;
            UserStatus = userData["EmployeeRoleName"] as string;
            UserVacationStatus = userData["EmployeeWorkingStatus"] as string;
            UserDateOfBirdth = userData["DateOfBirdth"] as DateTime?;
            UserPhoto = userData["UserImage"] as Image;
            Path = string.Empty;
            SetAcsessRigths();
            Save();
        }

        void SetAcsessRigths()
        {
            Dictionary<string, object> _userData = userData;
            var outerNew = Task<AcsessRigths?>.Factory.StartNew(() => new AcsessRigths(_userData["EmployeeRoleName"] as string));
            outerNew.Wait();
            GetAcsessRigths = outerNew.Result;
        }

        /// <summary>
        /// Метод сохраняет изображение в текущей папке
        /// </summary>
        void Save()
        {
            UserData tmpThis = this;

            var outerNew =  Task<string>.Factory.StartNew(() =>
            {
                string strFileName = tmpThis.UserFullName + "_" + tmpThis.UserLoginName;
                DirectoryInfo directoryInfo = Directory.CreateDirectory(Environment.CurrentDirectory + @"\Temp");
                var localFilePath = Environment.CurrentDirectory + @"\Temp\" + strFileName+ ".JPEG";

                if (!File.Exists(localFilePath))
                {
                     tmpThis.UserPhoto.Save(directoryInfo.ToString() + @"\" + strFileName + ".JPEG", System.Drawing.Imaging.ImageFormat.Jpeg);
                tmpThis.UserPhoto.Dispose();
                }

                tmpThis.Path = localFilePath;
                return localFilePath;
            });
            outerNew.Wait();
            Path = outerNew.Result;
        }

        /// <summary>
        /// Ссылка пользовательские данные
        /// </summary>
        private Dictionary<string, object> userData;

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserLoginName
        {
            get;
            private set;
        }

        public string Path { get; private set; }

        /// <summary>
        /// Имя , Фамилия пользователя
        /// </summary>
        public string UserFullName
        {
            get;
            private set;
        }

        ///// <summary>
        ///// Статус пользователя
        ///// </summary>
        public string UserStatus
        {
            get;
            private set ;
        }

        ///// <summary>
        ///// Статус (в отпуске или нет)
        ///// </summary>
        public string UserVacationStatus
        {
            get;
            private set;
        }

        ///// <summary>
        ///// Дата рождения
        ///// </summary>
        public DateTime? UserDateOfBirdth
        {
            get;
            private set;
        }

        ///// <summary>
        ///// Фото пользователя
        ///// </summary>
        public Image UserPhoto
        {
            get;
            private set;
        }

        /// <summary>
        /// Права доступа пользователя
        /// </summary>
        public AcsessRigths? GetAcsessRigths { get; private set; }
    }

    /// <summary>
    /// Права доступа пользователя
    /// </summary>
    public struct AcsessRigths
    {
        static AcsessRigths()
        {

        }

        public AcsessRigths(string userRole) 
        {
            this.UserRole = userRole;
            CardsCreate = false;
            ClientControl = false;
            ClientsControls = false;
            EmployeesEditing = false;
            TrainingControls = false;
            SeeTrainingList = false;
            FinanceAndServicesControls = false;
            AdministrationControls = false;
            GetRights();
        }

      

        /// <summary>
        /// Метод задает права пользователям
        /// </summary>
        private void GetRights()
        {
            try
            {
                if (string.IsNullOrEmpty(UserRole))
                    throw new Exception("В систему проник неизвестный пользователь!");

                switch (UserRole)
                {
                    case "Администратор":
                        CardsCreate = true;
                        ClientsControls = true;
                        EmployeesEditing = true;
                        TrainingControls = true;
                        SeeTrainingList = true;
                        FinanceAndServicesControls = true;
                        AdministrationControls = true;
                        break;
                    case "Фитнес-инструктор":
                        ClientsControls = true;
                        TrainingControls = true;
                        SeeTrainingList = true;
                        break;
                    case "Менеджер":
                        CardsCreate = true;
                        ClientsControls = true;
                        TrainingControls = true;
                        SeeTrainingList = true;
                        break;
                    case "Руководитель":
                        CardsCreate = true;
                        ClientsControls = true;
                        EmployeesEditing = true;
                        TrainingControls = true;
                        SeeTrainingList = true;
                        FinanceAndServicesControls = true;
                        break;
                    default:
                        throw new Exception("В систему проник неизвестный пользователь!"); 
                }
            }
            catch (Exception except)
            {
                Messenger.Default.Send<Exception>(except);
                Debug.Write("Исключение" + except.Message);
            }
        }


        /// <summary>
        /// Создание карт
        /// </summary>
        public bool CardsCreate { get; private set; }

        /// <summary>
        /// Управление записями клиентов
        /// </summary>
        public bool ClientControl { get; private set; }

        /// <summary>
        /// Роль пользователя системы
        /// </summary>
        private string UserRole { get; }

        /// <summary>
        /// Группа администрирование
        /// </summary>
        public bool AdministrationControls { get; set; }

        /// <summary>
        /// Группа финансы
        /// </summary>
        public bool FinanceAndServicesControls { get; private set; }

       /// <summary>
       /// Группа расписание занятий
       /// </summary>
        public bool SeeTrainingList { get; private set; }

        /// <summary>
        /// Группа Тренировка
        /// </summary>
        public bool TrainingControls { get; private set; }

        /// <summary>
        /// Группа Персонал (его редактирование)
        /// </summary>
        public bool EmployeesEditing { get; private set; }

        /// <summary>
        /// Группа Клиенты
        /// </summary>
        public bool ClientsControls { get; private set; }
    }

}
