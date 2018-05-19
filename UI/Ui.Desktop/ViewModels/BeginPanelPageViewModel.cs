using System;
using System.Drawing;
using System.Windows;
using FitnesClubCL;
using FitnesClubCL.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FitnessClubMWWM.Ui.Desktop.Pages;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    public class BeginPanelPageViewModel : ViewModelBase, ISystemUser
    {
        public BeginPanelPageViewModel()
        {
            //ButtonClick = new RelayCommand(ExecuteCommand);
            // _strUserRole = SimpleIoc.Default.GetInstance<AutorizationPageViewModel>().StrUserRole;
        }

        private readonly string _strUserRole;
        private string _strPath;


        #region Команды, отвечающие за показ страниц

        public RelayCommand ShowLogoutPageCommand => new RelayCommand(() => { Messenger.Default.Send("ConfrimExit"); });

        /// <summary>
        /// Команда показать панель администратора (доступна только для админа)
        /// </summary>
        public RelayCommand ShowAdminPanelCommand => new RelayCommand(() => { Messenger.Default.Send("AdminPage"); },
            () => 
            {
                if (WorkingUserData.HasValue) return WorkingUserData.Value.GetAcsessRigths.Value.AdministrationControls;
                //return false;
                return true;// На момент отладки
            });

        /// <summary>
        /// Команда меню "карты" (группа Карты)
        /// </summary>
        public RelayCommand ShowWorkingCabinetCommand => new RelayCommand(() =>{ Messenger.Default.Send("WorkingCabinetPage");},
            () =>
            {
                if (WorkingUserData.HasValue) return WorkingUserData.Value.GetAcsessRigths.Value.CardsCreate;
                // return false;.
                return true;// На момент отладки
            });
        
        /// <summary>
        /// Команда меню показать клиентов (группа клиенты)
        /// </summary>
        public RelayCommand ShowClientPageCommand => new RelayCommand(() => { Messenger.Default.Send("ClientPage"); },
            () => {
                if (WorkingUserData.HasValue) return WorkingUserData.Value.GetAcsessRigths.Value.ClientsControls;
                // return false;
                return true;// На момент отладки
            });

        /// <summary>
        /// Команда показать расписание трировок (группа расписание занятий)
        /// </summary>
        public RelayCommand ShowClassScheduleCommand =>
            new RelayCommand(() => { Messenger.Default.Send("ClassSchedule"); },
                () =>
                {
                    if (WorkingUserData.HasValue) return WorkingUserData.Value.GetAcsessRigths.Value.SeeTrainingList;
                    //return false;
                    return true;// На момент отладки
                });

        /// <summary>
        /// Команда показать информацию о персонале (группа Персонал) - доступна всем, кроме редактирования
        /// </summary>
        public RelayCommand ShowStaffPageCommand => new RelayCommand(() => { Messenger.Default.Send("StaffPage"); });

        /// <summary>
        /// Команда управления услуг и залами (группа Финансы, услуги, залы)
        /// </summary>
        public RelayCommand ShowSelectActionWindowCommand => new RelayCommand(() =>
        {
            Window selectActionWIndow = new SelectActionWindow();
            selectActionWIndow.ShowDialog();
        }, () =>
        {
            if (WorkingUserData.HasValue) return WorkingUserData.Value.GetAcsessRigths.Value.FinanceAndServicesControls;
            //return false;
            return true;// На момент отладки
        });

        #region Окно выбора действия
        /// <summary>
        /// Команда закрытия диалогового окна
        /// </summary>
        public RelayCommand<Window> CloseCommand { get; set; } =
            new RelayCommand<Window>((window) => { window?.Close(); });

        private delegate void ShowPages(Window wind, string strPageName);
        /// <summary>
        /// Показать страницу с установкой окладов работникам
        /// </summary>
        public RelayCommand<Window> ShowSalaryPageCommand { get; set; } = new RelayCommand<Window>(
            delegate (Window wind)
            {
                ShowPages del = InvokeShowPages;
                del(wind, "PayPage");
           
            });


        /// <summary>
        /// Показать страницу с установкой информации о тренировках
        /// </summary>
        public RelayCommand<Window> ShowPriceAbonementPageCommand { get; set; } = new RelayCommand<Window>(
            delegate(Window wind)
            {
                ShowPages del = InvokeShowPages;
                del(wind, "PriceAbonementPage");
            });

        /// <summary>
        /// Показать страницу со списком залов
        /// </summary>
        public RelayCommand<Window> ShowGymsPageCommand { get; set; } = new RelayCommand<Window>(
            delegate (Window wind)
            {
                ShowPages del = InvokeShowPages;
                del(wind, "GymPage");
            });

        //Метод, отвечающий за показ страниц
        static void InvokeShowPages(Window wind, string strPageName)
        {
            wind?.Close();
            Messenger.Default.Send(strPageName);
        }


        #endregion

        #endregion

        public RelayCommand ButtonAgreeWithExitClick => new RelayCommand(AgreeWithExit);

        public RelayCommand ButtonDisagreeWithExitClick => new RelayCommand(DisagreeWithExit);

        void ExecuteCommand()
        {
            MessageBox.Show("Выход из системы");
            Messenger.Default.Send("LoginPage");
        }

        private void AgreeWithExit()
        {
            Messenger.Default.Send("AgreeWithExit");
        }

        private void DisagreeWithExit()
        {
            Messenger.Default.Send("DisagreeWithExit");
        }


        /// <summary>
        /// Метод обновляет поля информации о пользователе
        /// </summary>
        public void SetUserData(UserData? workingUserData)
        {
            this.WorkingUserData = workingUserData;
            if (WorkingUserData != null)
            {
                UserFullName = WorkingUserData.Value.UserFullName;
                LoginName = WorkingUserData.Value.UserLoginName;
                DateOfBirdth = WorkingUserData.Value.UserDateOfBirdth;
                Status = WorkingUserData.Value.UserStatus;
                VacationStatus = WorkingUserData.Value.UserVacationStatus;
                UserPhoto = WorkingUserData.Value.UserPhoto;
                StrPath = WorkingUserData.Value.Path;
            }
        }

        public string UserFullName { get; private set; } 
        public string LoginName { get; private set; }
        public DateTime? DateOfBirdth { get; private set; }
        public string Status { get; private set; }
        //public DateTime TimeInSystem => (ModelManager.GetInstance() as ISystemUser).TimeInSystem;
        public string VacationStatus { get; private set; }
        public Image UserPhoto { get; private set; }
        public string StrPath { get; private set; }
       

        public AutorizationUserData AutorizationUserData { get; }
        public UserData? WorkingUserData { get; private set; }
    }
}
