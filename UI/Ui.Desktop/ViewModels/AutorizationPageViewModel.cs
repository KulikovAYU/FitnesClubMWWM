using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    class AutorizationPageViewModel : ViewModelBase
    {

        public RelayCommand Autentification => new RelayCommand(()=>{ Messenger.Default.Send("MainPage");});
    }
}
