using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnesClubCL;
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

        /// <summary>
        /// Предстоящие тренировки с учетом наличия этого типа тренировок в абонементе
        /// </summary>
        public ObservableCollection<UpcomingTraining> _AvailableTrainings { get; set; }

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


        private ObservableCollection<ServicesInSubscription> _arrServicesInSubscription;

        /// <summary>
        /// Список доступных услуг в абонементе
        /// </summary>
        public  ObservableCollection<ServicesInSubscription> ArrServicesInSubscription 
        {
            get
            {
                if (_Account?.Abonement != null)
                  return  _arrServicesInSubscription = ModelManager.GetInstance.GetServicesInSubscription(_Account);
                return null;
            }
            set => _arrServicesInSubscription = value;
        }

        private ObservableCollection<UpcomingTraining> _arrUpcomingTraining;
        /// <summary>
        /// Список предстоящих тренировок
        /// </summary>
        public  ObservableCollection<UpcomingTraining> ArrUpcomingTraining
        {
            get
            {
                if (_Account?.Abonement != null)
                    return _arrUpcomingTraining = ModelManager.GetInstance.GetUpcomingTraining(_Account);
               
                return null;
            }
            set => _arrUpcomingTraining = value;
        }

        /// <summary>
        /// Тренировки в абонементе
        /// </summary>
        public ServicesInSubscription _ServicesInSubscription { get; set; }

        /// <summary>
        /// Ссылка на окно записи
        /// </summary>
        private CreateRecordWindow recordWindow;
        /// <summary>
        /// Записаться на тренировку
        /// </summary>
        public RelayCommand<ServicesInSubscription> CreateRecordCommand => new RelayCommand<ServicesInSubscription>((selectedItem) =>
        {
            _ServicesInSubscription = selectedItem as ServicesInSubscription;
            recordWindow = new CreateRecordWindow();
            _AvailableTrainings = ModelManager.GetInstance.GetAvailableTrainings(_ServicesInSubscription);
            recordWindow.ShowDialog();
        });


        /// <summary>
        /// Выбранная тренировка
        /// </summary>
        public UpcomingTraining _CurrentItem { get; set; }

        /// <summary>
        /// Создание записи на тренировку (через DataGrid)
        /// </summary>
        public RelayCommand CreateVisitCommand => new RelayCommand(
            () =>
            {
                //1. Создали предварительную регистрацию
                ModelManager.GetInstance.CreatePriorRegistration(_Account,_ServicesInSubscription, _CurrentItem);
                //2. Обновили поле Доступные тренировки
                ArrServicesInSubscription = ModelManager.GetInstance.GetServicesInSubscription(_Account);
                //3. Обновили список записей (если останется время отфильтровать все тренировки, которые есть уже в записи)
                ArrUpcomingTraining = ModelManager.GetInstance.GetUpcomingTraining(_Account);
                //4. Закроем диалог
                recordWindow.Close();
            }, ModelManager.GetInstance.CheckTrainingOnAvailable(_CurrentItem)  );


        /// <summary>
        /// Отметка о посещении тренировки
        /// </summary>
        public RelayCommand<UpcomingTraining> FixTheVisitCommand => new RelayCommand<UpcomingTraining>((upcTraining) =>
            {
                ModelManager.GetInstance.FixTheVisit(_Account,upcTraining as UpcomingTraining);
                ArrUpcomingTraining = ModelManager.GetInstance.GetUpcomingTraining(_Account);

            });


        /// <summary>
        /// Отказ о посещении тренировки
        /// </summary>
        public RelayCommand<UpcomingTraining> RefusalOfVisitCommand => new RelayCommand<UpcomingTraining>(
            (upcTraining) =>
            {

                ModelManager.GetInstance.RefusalOfVisit(_Account, upcTraining);
                //2. Обновили поле Доступные тренировки
                ArrServicesInSubscription = ModelManager.GetInstance.GetServicesInSubscription(_Account);
                //3. Обновим список записей
                ArrUpcomingTraining = ModelManager.GetInstance.GetUpcomingTraining(_Account);

            });

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
