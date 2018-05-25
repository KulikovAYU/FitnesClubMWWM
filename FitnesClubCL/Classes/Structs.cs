/// <summary>
/// Данный файл описывает структуру, которая передается вью модели
/// </summary>

using System;
using System.Diagnostics;
using GalaSoft.MvvmLight.Messaging;

namespace FitnesClubCL.Classes
{



    //TODO:потом убить
    /// <summary>
    /// Права доступа пользователя потом убить
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
    
}
