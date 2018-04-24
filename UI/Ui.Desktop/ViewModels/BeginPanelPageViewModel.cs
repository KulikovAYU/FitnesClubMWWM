using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop
{
    public  class BeginPanelPageViewModel : ViewModelBase
    {
        public BeginPanelPageViewModel()
        {
            //ButtonClick = new RelayCommand(ExecuteCommand);
        }

        #region Команды, отвечающие за показ страниц

        public RelayCommand ShowLogoutPageCommand => new RelayCommand(() => { Messenger.Default.Send("ConfrimExit"); });

        public RelayCommand ShowAdminPanelCommand => new RelayCommand(() => { Messenger.Default.Send("AdminPage"); });

        public RelayCommand ShowWorkingCabinetCommand => new RelayCommand(() => { Messenger.Default.Send("WorkingCabinetPage");});

        public RelayCommand ShowClientPageCommand => new RelayCommand(() => { Messenger.Default.Send("ClientPage"); });


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
