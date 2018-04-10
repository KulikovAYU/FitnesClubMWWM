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
        
        public RelayCommand ButtonClick => new RelayCommand(ExecuteCommand);


        void ExecuteCommand()
        {
            MessageBox.Show("Выход из системы");
            Messenger.Default.Send("LoginPage");
        }

    }
}
