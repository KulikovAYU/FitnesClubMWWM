using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnessClubMWWM.Ui.Desktop.DataModels;
using FitnessClubMWWM.Ui.Desktop.Pages.Wind;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    /// <summary>
    /// Вью модель для страницы информации об абонементе
    /// </summary>
    public class AbonementInfoViewModel : ViewModelBase
    {

        public Account _Account { get; set; }

        public AbonementInfoViewModel()
        {
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }


        // <summary>
        // Текущая страница приложения
        // </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.AbonementInfoDetails; // ApplicationPage.Login;  ApplicationPage.MainPage


        void ProcessMessage(string msg)
        {

            switch (msg)
            {

                case "ShowAbonementInfo":
                    CurrentPage = ApplicationPage.AbonementInfoDetails;
                    break;
                case "ShowTrainingAndServiceListCommand":
                    CurrentPage = ApplicationPage.TrainingAndServiceList;
                    break;
                case "LongAbonement":
                    CurrentPage = ApplicationPage.LongAbonement;
                    break;
                case "PayServicesAndTrainings":
                    CurrentPage = ApplicationPage.PayServicesAndTrainings;
                    break;
            }
        }
        public RelayCommand ShowAbonementInfoCommand => new RelayCommand(() => Messenger.Default.Send("ShowAbonementInfo"));
        public RelayCommand ShowTrainingAndServiceListCommand => new RelayCommand(() => Messenger.Default.Send("ShowTrainingAndServiceListCommand"));

        /// <summary>
        /// Крманда продать абонемент (из таблицы)
        /// </summary>
        public RelayCommand<Account> SellTrainingOrServiceCommand => new RelayCommand<Account>((item) => {
            Messenger.Default.Send("PayServicesAndTrainings");
        });


        public RelayCommand<Account> LongAbonementRelayCommand => new RelayCommand<Account>((account)=>
        {
            Messenger.Default.Send("LongAbonement");
         });

        public RelayCommand<Account> ActivateAbonementRelayCommand => new RelayCommand<Account>((account) =>
        {
         
        });

    }
}
