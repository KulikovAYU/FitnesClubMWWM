using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FC_EMDB.EMDB.CF.Data.Domain;

namespace FC_EMDB.Classes
{
    /// <summary>
    /// "Авторизационные данные пользователя"
    /// </summary>
    public class AutorizationUserData
    {
        public AutorizationUserData()
        {

        }
        /// <summary>
        /// Логин
        /// </summary>
        public string UserLoginName { get; set; }
        /// <summary>
        /// строка пароля
        /// </summary>
        public string PasswordString { get; set; }
        /// <summary>
        /// сгенерированный хэш пароля
        /// </summary>
        public string PasswordHash { get;  private set; }

        /// <summary>
        /// Установить хэш пароля
        /// </summary>
        /// <param name="pwdHash"></param>
        public void SetPasswordHash(string pwdHash)
        {
            if (!string.IsNullOrEmpty(PasswordString))
            {
                PasswordHash = pwdHash;
            }
        }
    }
    
    /// <summary>
    /// данные пользователя после авторизации
    /// </summary>
    public class UserData
    {
        public UserData()
        {

        }

        /// <summary>
        /// Конструктор основных личных данных работника
        /// </summary>
        /// <param name="employeeId">Id работника</param>
        /// <param name="employeeFirstName">Имя работника</param>
        /// <param name="employeeLastName">Отчество работника</param>
        /// <param name="employeeFamilyName">Фамилия работника</param>
        /// <param name="employeeDateOfBirdth">Дата рождения работника</param>
        /// <param name="employeeAdress">Адрес работника</param>
        private UserData(int employeeId, string employeeFirstName, string employeeLastName, string employeeFamilyName, DateTime employeeDateOfBirdth, string employeeAdress)
        {
            EmployeeId = employeeId;
            EmployeeFirstName = employeeFirstName;
            EmployeeLastName = employeeLastName;
            EmployeeFamilyName = employeeFamilyName;
            EmployeeDateOfBirdth = employeeDateOfBirdth;
            EmployeeAdress = employeeAdress;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="employeeId">Id работника</param>
        /// <param name="employeeFirstName">Имя работника</param>
        /// <param name="employeeLastName">Отчество работника</param>
        /// <param name="employeeFamilyName">Фамилия работника</param>
        /// <param name="employeeDateOfBirdth">Дата рождения работника</param>
        /// <param name="employeeAdress">Адрес проживания работника</param>
        /// <param name="employeeLoginName">Логин работника</param>
        /// <param name="employeePasswordHash">Хэш пароля работника</param>
        /// <param name="employeeMail">Мейл работника</param>
        /// <param name="employeePhoneNumber">Телефон работника</param>
        /// <param name="employeeRole">Роль работника</param>
        /// <param name="employeeWorkingStatus">Рабочий статус работника</param>
        /// <param name="employeePhoto">Фотография работника</param>
        public UserData(int employeeId, string employeeFirstName, string employeeLastName, string employeeFamilyName, DateTime employeeDateOfBirdth, string employeeAdress, 
                string employeeLoginName, string employeePasswordHash, string employeeMail, string employeePhoneNumber, EmployeeRole employeeRole, string employeeWorkingStatus, 
                byte[] employeePhoto) : this(employeeId, employeeFirstName,  employeeLastName,  employeeFamilyName, employeeDateOfBirdth, employeeAdress)
        {
            EmployeeLoginName = employeeLoginName;
            EmployeePasswordHash = employeePasswordHash;
            EmployeeMail = employeeMail;
            EmployeePhoneNumber = employeePhoneNumber;
            EmployeRole= employeeRole;
            EmployeWorkingStatus = employeeWorkingStatus;
            EmployeePhoto = employeePhoto;
            SavePhoto();
        }


        public string EmployeeAdress { get; private set; }

        public DateTime EmployeeDateOfBirdth { get; private set; }

        public string EmployeeFamilyName { get; private set; }

        public string EmployeeLastName { get; private set; }

        public string EmployeeFirstName { get; private set; }

        public int EmployeeId { get; private set; }

        public byte[] EmployeePhoto { get; private set; }

        public string EmployeWorkingStatus { get; private set; }

        public EmployeeRole EmployeRole { get; private set; }

        public string EmployeePhoneNumber { get; private set; }

        public string EmployeeMail { get; private set; }

        public string EmployeePasswordHash { get; private set; }

        public string EmployeeLoginName { get; private set; }

        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string EmployeeFullName => EmployeeFirstName+" "+EmployeeLastName+" "+EmployeeFamilyName;

        ///// <summary>
        ///// Фото пользователя
        ///// </summary>
        public Image UserPhoto { get; private set; }

        /// <summary>
        /// Путь к фотографии пользователя системы
        /// </summary>
        public string PathPhoto { get; private set; }

        /// <summary>
        /// Метод сохраняет изображение в текущей папке
        /// </summary>
        void SavePhoto()
        {
            UserData tmpThis = this;

            var outerNew = Task<string>.Factory.StartNew(() =>
            {
                string strFileName = tmpThis.EmployeeFirstName + "_" + tmpThis.EmployeeLastName +"_"+tmpThis.EmployeeId;
                string savePathFolder = $@"{Environment.CurrentDirectory}\{"Temp"}\{tmpThis.EmployeRole.EmployeeRoleName}";
                DirectoryInfo directoryInfo = Directory.CreateDirectory(savePathFolder);
                string localFilePath = $@"{savePathFolder}\{strFileName}.{"JPEG"}";

                if (!File.Exists(localFilePath))
                {
                    MemoryStream memoryStream = new MemoryStream();
                    memoryStream.Write(EmployeePhoto, 0, EmployeePhoto.Length);
                    UserPhoto = Image.FromStream(memoryStream);
                    tmpThis.UserPhoto.Save(localFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    memoryStream.Dispose();
                    tmpThis.UserPhoto.Dispose();
                }

                return localFilePath;
            });
            outerNew.Wait();
            PathPhoto = outerNew.Result;
        }
    }
}
