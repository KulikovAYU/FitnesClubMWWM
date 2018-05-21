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
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace FitnesClubCL.Classes
{
   

  

    /// <summary>
    /// Права доступа пользователя
    /// </summary>
    public class AcsessRigths
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

        public AcsessRigths()
        {
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

    /// <summary>
    /// Регистрационные данные новго клиента
    /// </summary>
    public class NewClientData 
    {
       
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string ClientFamilyName { get; set; }
        /// <summary>
        /// Отчество клиента
        /// </summary>
        public string ClientLastName { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string ClientPhoneNumber { get; set; }
        /// <summary>
        /// Пол клиента
        /// </summary>
        public string ClientGender { get; set; }

        public DateTime? ClientDateOfBirdth { get; set; }

        #region Паспортные данные клиента
        /// Серия
        public string ClientPasportDataSeries { get; set; }
        /// <summary>
        /// Номер
        /// </summary>
        public string ClientPasportDataNumber { get; set; }
        /// <summary>
        /// Кем выдан
        /// </summary>
        public string ClientPasportDataIssuedBy { get; set; }
        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? ClientPasportDatеOfIssue { get; set; }
        #endregion

        /// <summary>
        /// Е-мейл клиента
        /// </summary>
        public string ClientEmail { get; set; }

        /// <summary>
        /// Путь к фото клиента
        /// </summary>

        string ConvertImage()
        {
            NewClientData tmpThis = this;

            var outerNew = Task<string>.Factory.StartNew(() =>
            {
                string strFileName = tmpThis.ClientName + "_" + tmpThis.ClientFamilyName + "_" + tmpThis.ClientLastName;
                DirectoryInfo directoryInfo = Directory.CreateDirectory(Environment.CurrentDirectory + @"\Temp");
                var localFilePath = Environment.CurrentDirectory + @"\Temp\" + strFileName + ".JPEG";

                if (!File.Exists(localFilePath))
                {
                    tmpThis.ClientPhoto.Save(directoryInfo.ToString() + @"\" + strFileName + ".JPEG", System.Drawing.Imaging.ImageFormat.Jpeg);
                    tmpThis.ClientPhoto.Dispose();
                }

                //tmpThis.ClientPhotoPath = localFilePath;
                return localFilePath;
            });
            outerNew.Wait();
            return outerNew.Result;
        }

      
        /// <summary>
        /// Фото клиента
        /// </summary>
        public Image ClientPhoto { get; set; }

        /// <summary>
        /// Путь к фото пользователя
        /// </summary>
        public string ClientPhotoPath
        {
            get => ClientPhoto != null ? ConvertImage() : string.Empty;
            set => ClientPhotoPath = value;
        }

        public string GetPath()
        {
            NewClientData tmpThis = this;

            var outerNew = Task<string>.Factory.StartNew(() =>
            {
                string strFileName = tmpThis.ClientName + "_" + tmpThis.ClientFamilyName + "_" + tmpThis.ClientLastName;
                DirectoryInfo directoryInfo = Directory.CreateDirectory(Environment.CurrentDirectory + @"\Temp");
                var localFilePath = Environment.CurrentDirectory + @"\Temp\" + strFileName + ".JPEG";

                if (!File.Exists(localFilePath))
                {
                    tmpThis.ClientPhoto.Save(directoryInfo.ToString() + @"\" + strFileName + ".JPEG", System.Drawing.Imaging.ImageFormat.Jpeg);
                    tmpThis.ClientPhoto.Dispose();
                }

              //  tmpThis.ClientPhotoPath = localFilePath;
                return localFilePath;
            });
            outerNew.Wait();
            return outerNew.Result;
        }

        public bool IsExistClient { get; set; }
    }
    
}
