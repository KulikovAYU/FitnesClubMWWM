using System;
using System.Drawing;
using System.Windows;
using FitnesClubCL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FitnessClubMWWM.Ui.Desktop.Pages;
using GalaSoft.MvvmLight.Ioc;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    public class BeginPanelPageViewModel : ViewModelBase, ISystemUser
    {
        public BeginPanelPageViewModel()
        {
            //ButtonClick = new RelayCommand(ExecuteCommand);
            _strUserRole = SimpleIoc.Default.GetInstance<AutorizationPageViewModel>().StrUserRole;
        }

        private readonly string _strUserRole;


        #region Команды, отвечающие за показ страниц

        public RelayCommand ShowLogoutPageCommand => new RelayCommand(() => { Messenger.Default.Send("ConfrimExit"); });

        /// <summary>
        /// Команда показать панель администратора (доступна только для админа)
        /// </summary>
        public RelayCommand ShowAdminPanelCommand => new RelayCommand(() => { Messenger.Default.Send("AdminPage"); },
            () => _strUserRole != null && _strUserRole.Equals("Администратор"));

        public RelayCommand ShowWorkingCabinetCommand => new RelayCommand(() =>
        {
            Messenger.Default.Send("WorkingCabinetPage");
        });

        public RelayCommand ShowClientPageCommand => new RelayCommand(() => { Messenger.Default.Send("ClientPage"); });

        public RelayCommand ShowClassScheduleCommand =>
            new RelayCommand(() => { Messenger.Default.Send("ClassSchedule"); });

        public RelayCommand ShowStaffPageCommand => new RelayCommand(() => { Messenger.Default.Send("StaffPage"); });

        public RelayCommand ShowSelectActionWindowCommand => new RelayCommand(() =>
        {
            Window selectActionWIndow = new SelectActionWindow();
            selectActionWIndow.ShowDialog();
        }, () => _strUserRole != null && (_strUserRole != null && _strUserRole.Equals("Руководитель") ||
                                          _strUserRole.Equals("Администратор")));

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


        public string UserFullName { get; set; }
        public string LoginName => (ModelManager.GetInstance() as ISystemUser).LoginName;
        public DateTime? DateOfBirdth => (ModelManager.GetInstance() as ISystemUser).DateOfBirdth;
        public string Status => (ModelManager.GetInstance() as ISystemUser).Status;
        public DateTime TimeInSystem => (ModelManager.GetInstance() as ISystemUser).TimeInSystem;
        public string VacationStatus => (ModelManager.GetInstance() as ISystemUser).VacationStatus;
        public Image UserPhoto => (ModelManager.GetInstance() as ISystemUser).UserPhoto;
    }
}
