using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FitnessClubMWWM.Ui.Desktop.Pages;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    public class BeginPanelPageViewModel : ViewModelBase
    {
        public BeginPanelPageViewModel()
        {
            //ButtonClick = new RelayCommand(ExecuteCommand);
        }


        #region Команды, отвечающие за показ страниц

        public RelayCommand ShowLogoutPageCommand => new RelayCommand(() => { Messenger.Default.Send("ConfrimExit"); });

        public RelayCommand ShowAdminPanelCommand => new RelayCommand(() => { Messenger.Default.Send("AdminPage"); });

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


    }
}
