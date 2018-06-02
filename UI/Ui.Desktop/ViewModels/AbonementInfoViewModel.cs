using System;
using System.Collections.ObjectModel;
using System.Windows;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnessClubMWWM.Ui.Desktop.DataModels;
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

        /// <summary>
        /// Номер абонемента
        /// </summary>
        private string _numberSubscription;

        public string NumberSubscription
        {
            get => _Account?.Abonement.NumberSubscription.ToString();
            set => _numberSubscription = value;
        }

        public AbonementInfoViewModel()
        {
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }




        public  ObservableCollection<ServicesInSubscription> ArrServicesInSubscription 
        {
            get
            {
                var xx = _Account;
            //var x = _Account?.Abonement?.ArrServicesInSubscription.Cast<ServicesInSubscription>();
                if (_Account?.Abonement != null)
                    return new ObservableCollection<ServicesInSubscription>(_Account?.Abonement
                        ?.ArrServicesInSubscription);
                return null;
            }
            
        }


        // <summary>
        // Текущая страница приложения
        // </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.AbonementInfoDetails; // ApplicationPage.Login;  ApplicationPage.MainPage

        /// <summary>
        /// Страницы
        /// </summary>
        /// <param name="msg"></param>
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



        /// <summary>
        /// Команда закрытия диалогового окна
        /// </summary>
        public RelayCommand<Window> CloseCommand { get; set; } =
            new RelayCommand<Window>((window) => { window?.Close(); });


        public int ResizeBorder { get; set; } = 6;
      

        public void SetData<T>(T account)
        {
            _Account = account as Account;
        }
    }
}
