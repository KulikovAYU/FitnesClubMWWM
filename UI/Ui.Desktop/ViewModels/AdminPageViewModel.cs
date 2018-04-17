using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FitnessClubMWWM.Ui.Desktop.Pages.Wind;
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
            bVisibility = Visibility.Visible;
           // RaisePropertyChanged(nameof(bVisibility));
            MessageBox.Show("Сработало");
            Application.Current.Dispatcher.Invoke(() => { new Window1().ShowDialog();});
        }

        public Visibility bVisibility { get; set; } = Visibility.Hidden;


    }
}
