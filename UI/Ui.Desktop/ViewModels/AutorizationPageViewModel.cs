using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
   public class AutorizationPageViewModel : ViewModelBase
    {

        public RelayCommand Autentification => new RelayCommand(()=>{ Messenger.Default.Send("MainPage");});
    }
}
