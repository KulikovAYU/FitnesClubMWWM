using System.Windows;
using FitnessClubMWWM.Ui.Desktop;
using GalaSoft.MvvmLight;
using FitnessClubMWWM.Ui.Desktop.Pages.Wind;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Logic.Ui
{
    public class AdminPageViewModel : ViewModelBase
    {

        //public RelayCommand CloseCommand => new RelayCommand((() => { wind.Close();}));
 
        public RelayCommand GoHomeCommand => new RelayCommand(() => { Messenger.Default.Send("MainPage");});


       public RelayCommand RegisterNewUserCommand => new RelayCommand(() =>{CurrentPage = ApplicationPage.AddNewUserPage;

           CustomMessageBox.Show("sdds","Сообщение",MessageBoxButton.OK,eMessageBoxIcons.eWarning); });

        public RelayCommand AddNewUserPrivilegyCommand => new RelayCommand(() => { CurrentPage = ApplicationPage.AddUserPrivilegyPage;});

        //private CustomMessageBox wind;
        //void RegisterNewUser()
        //{

        //    //Messenger.Default.Send("ShowAddNewUserPage");
        //   // RaisePropertyChanged(nameof(bVisibility));
        //   // MessageBox.Show("Сработало");
           
        //    //Application.Current.Dispatcher.Invoke(() =>
        //    //{
        //    //    try
        //    //    {
        //    //        wind = new CustomMessageBox();
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
