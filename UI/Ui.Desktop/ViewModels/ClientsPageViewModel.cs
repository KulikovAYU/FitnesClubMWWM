using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnesClubCL;
using FitnessClubMWWM.Ui.Desktop.Constants;
using FitnessClubMWWM.Ui.Desktop.Pages;
using FitnessClubMWWM.Ui.Desktop.Pages.Wind;
using FitnessClubMWWM.Ui.Desktop.Pages.Wind.AbonementInfo;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    public class ClientsPageViewModel : ViewModelBase
    {


       
        public Account _Account { get; private set; }

      

     

      

     

        private  ObservableCollection<Service> _serviceLists = null;

        public ObservableCollection<Service> ServiceLists => _serviceLists ?? ModelManager.GetReferenceData<Service>();

        public ObservableCollection<AccountStatus> _accountStatuses = null;

        public ObservableCollection<AccountStatus> AccountStatuses
        {
            get
            {
                if (_accountStatuses != null)
                    return _accountStatuses;

                _accountStatuses = ModelManager.GetReferenceData<AccountStatus>();

                return _accountStatuses;
            }
        }

      

        ///// <summary>
        ///// Крманда продать абонемент (из таблицы)
        ///// </summary>
        //public RelayCommand<Account> SellNewAbonementCommand => new RelayCommand<Account>((item) => {
        //    Window selectActionWIndow = new PayAbonement();
        //    _Account = item;
        //    selectActionWIndow.ShowDialog();
        //});


        /// <summary>
        /// Крманда подробно (из таблицы)
        /// </summary>
        public RelayCommand<Account> ShowAbonementInfoCommand => new RelayCommand<Account>((item) => {
            Window selectActionWIndow = new AbonementInfo();
            SimpleIoc.Default.GetInstance<AbonementInfoViewModel>().SetData<Account>(item as Account);
            selectActionWIndow.ShowDialog();
        });

        public RelayCommand RegisterNewClientCommand => new RelayCommand(() => { Messenger.Default.Send("RegisterNewClientPage"); });

        public RelayCommand ShowCheckClubCardWindow =>
            new RelayCommand(() => (new CheckTheClubCardWindow()).ShowDialog());

        public ClientsPageViewModel()
        {
         

            //  Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }

        public RelayCommand GoHomeCommand => new RelayCommand(() => { Messenger.Default.Send("MainPage"); });


        public Visibility visibleSearchPanel { get; set; } = Visibility.Hidden;

        public RelayCommand FindClientCommand =>
            new RelayCommand((() => {
                if (visibleSearchPanel == Visibility.Hidden)
                {
                    visibleSearchPanel = Visibility.Visible;
                }

                }));

 public RelayCommand HideSearchPanel =>
            new RelayCommand((() => {
                if (visibleSearchPanel == Visibility.Visible)
                {
                    visibleSearchPanel = Visibility.Hidden;
                }

                }));


        private static bool _isAv { get; set; }

        public RelayCommand<bool> ActivateRelayCommand { get; set; } =
        new RelayCommand<bool>((ob) => { _isAv = ob;  }, _isAv);

        /// <summary>
        /// Данная команда вызывается из таблицы клиентов и передает данные в форму для корректировки данных клиента
        /// </summary>
        public RelayCommand<Account> CorretAccountData =>new RelayCommand<Account>((item) => {
            Messenger.Default.Send("RegisterNewClientPage");
            SimpleIoc.Default.GetInstance<WorkingCabinetPageViewModel>().ExistAccount = item;
            SimpleIoc.Default.GetInstance<WorkingCabinetPageViewModel>().UpdatePageFields(item);
         
        });




       
        /// <summary>
        /// Список видов услуг
        /// </summary>
        public ObservableCollection<PriceTrainingList> ArrPriceTrainingList => ModelManager.GetReferenceData<PriceTrainingList>();

        public PriceTrainingList CurrenPriceTraining { get; set; }

        public ObservableCollection<Tarif> ArrTarifs => ModelManager.GetReferenceData<Tarif>();

        public int nTrainingCount { get; set; }

        public Decimal TotalCost { get; set; }

        public RelayCommand CreateNewServiceInAbonementCommand =>new RelayCommand(() =>
       {
           ServicesInSubscription newRecord = new ServicesInSubscription()
           {
              
           };
       });

        public class Clients //Отладка
        {
            ///Пока просто для отладки
            public Image Photo { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string FatherName { get; set; }

            public string TypeAbonement { get; set; }

            public string PhoneNumber { get; set; }

        }


       
        private ObservableCollection<Clients> clientsList = null; //Отладка

        public ObservableCollection<Account> _clientsList { get; set; } = null;

        public ObservableCollection<Account> GetClientsList()
        {
            _clientsList = ModelManager.GetAllPersons<Account>();
            return _clientsList;
        }
        /// <summary>
        /// Список клиентов
        /// </summary>
        //public ObservableCollection<Account> _ClientsList
        //{
        //    get
        //    {
        //        //if (_clientsList != null)
        //        //    return _clientsList;

        //        _clientsList = ModelManager.GetAllPersons<Account>();
              
        //        return _clientsList;
        //    }
        //}

        public ObservableCollection<Clients> ClientsList
        {
            get
            {
                if (clientsList != null)
                    return clientsList;

                clientsList = new ObservableCollection<Clients>
                {
                    new Clients()
                    {
                        Photo = null,
                        LastName = "Куликов",
                        FirstName = "Антон",
                        FatherName = "Юрьевич",
                        TypeAbonement = "Vip",
                        PhoneNumber = "8-920-672-0-68"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "РПОРрыов",
                        FirstName = "пав",
                        FatherName = "пав",
                        TypeAbonement = "Норма",
                        PhoneNumber = "8-920-672-0-32"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "Куликов",
                        FirstName = "Антон",
                        FatherName = "Юрьевич",
                        TypeAbonement = "Vip",
                        PhoneNumber = "8-920-672-0-68"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "РПОРрыов",
                        FirstName = "пав",
                        FatherName = "пав",
                        TypeAbonement = "Норма",
                        PhoneNumber = "8-920-672-0-32"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "Куликов",
                        FirstName = "Антон",
                        FatherName = "Юрьевич",
                        TypeAbonement = "Vip",
                        PhoneNumber = "8-920-672-0-68"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "РПОРрыов",
                        FirstName = "пав",
                        FatherName = "пав",
                        TypeAbonement = "Норма",
                        PhoneNumber = "8-920-672-0-32"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "Куликов",
                        FirstName = "Антон",
                        FatherName = "Юрьевич",
                        TypeAbonement = "Vip",
                        PhoneNumber = "8-920-672-0-68"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "РПОРрыов",
                        FirstName = "пав",
                        FatherName = "пав",
                        TypeAbonement = "Норма",
                        PhoneNumber = "8-920-672-0-32"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "Куликов",
                        FirstName = "Антон",
                        FatherName = "Юрьевич",
                        TypeAbonement = "Vip",
                        PhoneNumber = "8-920-672-0-68"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "РПОРрыов",
                        FirstName = "пав",
                        FatherName = "пав",
                        TypeAbonement = "Норма",
                        PhoneNumber = "8-920-672-0-32"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "Куликов",
                        FirstName = "Антон",
                        FatherName = "Юрьевич",
                        TypeAbonement = "Vip",
                        PhoneNumber = "8-920-672-0-68"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "РПОРрыов",
                        FirstName = "пав",
                        FatherName = "пав",
                        TypeAbonement = "Норма",
                        PhoneNumber = "8-920-672-0-32"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "Куликов",
                        FirstName = "Антон",
                        FatherName = "Юрьевич",
                        TypeAbonement = "Vip",
                        PhoneNumber = "8-920-672-0-68"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "РПОРрыов",
                        FirstName = "пав",
                        FatherName = "пав",
                        TypeAbonement = "Норма",
                        PhoneNumber = "8-920-672-0-32"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "Куликов",
                        FirstName = "Антон",
                        FatherName = "Юрьевич",
                        TypeAbonement = "Vip",
                        PhoneNumber = "8-920-672-0-68"
                    },
                    new Clients()
                    {
                        Photo = null,
                        LastName = "РПОРрыов",
                        FirstName = "пав",
                        FatherName = "пав",
                        TypeAbonement = "Норма",
                        PhoneNumber = "8-920-672-0-32"
                    }
                };
                return clientsList;
            }
        } //Отладка

        /// <summary>
        /// абонемент клиенгта
        /// </summary>
        public class Abonement//Отладка
        {
            public Abonement()
            {
                m_nNumber++;
            }

            public static int m_nNumber { get; set; } = 0;//номер абонемента
            public string m_strStatus { get; set; } //статус абонемента
            public string m_strService { get; set; }//услуга
            public string m_strTariff { get; set; }//тариф
            public int m_nAllCntTrainings { get; set; }//куплено трениовок
            public int m_nRemainedTrainings { get; set; }//осталось трениовок
        }

       
        private ObservableCollection<Abonement> m_abonementList = null; //Отладка

        public ObservableCollection<Abonement> GetAbonement //Отладка
        {
            get
            {
                if (m_abonementList != null)
                    return m_abonementList;

                m_abonementList = new ObservableCollection<Abonement>
                {
                    new Abonement()
                    {
                        m_strStatus = "Активна",
                        m_strService = "VIP-карта",
                        m_strTariff = "Весь день",
                        m_nAllCntTrainings = 10,
                        m_nRemainedTrainings = 5
                    }
                };
                return m_abonementList;
            }
        }

 
    }
}
