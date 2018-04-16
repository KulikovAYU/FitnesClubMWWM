using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Logic.Ui
{
    public class AdminPageViewModel : ViewModelBase
    {

        public RelayCommand GoHomeCommand => new RelayCommand(GoHome);


        void GoHome()
        {
            Messenger.Default.Send("MainPage");
        }

        public RelayCommand RegisterNewUserCommand => new RelayCommand(RegisterNewUser);

        void RegisterNewUser()
        {
            MessageBox.Show("Сработало");
        }

      

    }
}
