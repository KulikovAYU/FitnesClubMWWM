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

        public RelayCommand RegisterNewUserCommand => new RelayCommand(() =>
        {
            CurrentPage = ApplicationPage.AddNewUserPage;});

        public RelayCommand AddNewUserPrivilegyCommand => new RelayCommand(() => { CurrentPage = ApplicationPage.AddUserPrivilegyPage; });

        private Window1 wind;
        //void RegisterNewUser()
        //{

        //    //Messenger.Default.Send("ShowAddNewUserPage");
        //   // RaisePropertyChanged(nameof(bVisibility));
        //   // MessageBox.Show("Сработало");
           
        //    //Application.Current.Dispatcher.Invoke(() =>
        //    //{
        //    //    try
        //    //    {
        //    //        wind = new Window1();
        //    //        wind.ShowDialog();
        //    //    }
        //    //    catch (Exception e)
        //    //    {
        //    //        Console.WriteLine(e);
        //    //        throw;
        //    //    }
        //    //});
        //    CurrentPage = ApplicationPage.AddNewUserPage;
        //    //RaisePropertyChanged(nameof(AdminPageViewModel));
        //}
        ////public RelayCommand CloseSecWindow => new RelayCommand(Close);

        ////void Close()
        ////{
        ////   wind.Close();
        ////}


        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.AddUserPrivilegyPage;



    }
}
