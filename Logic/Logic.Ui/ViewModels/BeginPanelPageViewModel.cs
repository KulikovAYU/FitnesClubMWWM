using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Logic.Ui
{
    public  class BeginPanelPageViewModel : ViewModelBase
    {
        public BeginPanelPageViewModel()
        {
            //ButtonClick = new RelayCommand(ExecuteCommand);
        }
        
        public RelayCommand ButtonClick => new RelayCommand(LogoutCommand);

        public RelayCommand ButtonAgreeWithExitClick => new RelayCommand(AgreeWithExit);

        public RelayCommand ButtonDisagreeWithExitClick => new RelayCommand(DisagreeWithExit);

        public RelayCommand AdminPanelClick => new RelayCommand(ShowAdminPanel);


        void ExecuteCommand()
        {
            MessageBox.Show("Выход из системы");
            Messenger.Default.Send("LoginPage");
        }

        private void LogoutCommand()
        {
            Messenger.Default.Send("ConfrimExit");
        }

        private void AgreeWithExit()
        {
            Messenger.Default.Send("AgreeWithExit");
        }

        private void DisagreeWithExit()
        {
            Messenger.Default.Send("DisagreeWithExit");
        }

        private void ShowAdminPanel()
        {
            Messenger.Default.Send("AdminPage");
        }

    }
}
