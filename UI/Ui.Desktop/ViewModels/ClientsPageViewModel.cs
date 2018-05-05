using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using FitnessClubMWWM.Ui.Desktop.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    public class ClientsPageViewModel : ViewModelBase
    {


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


        //public RelayCommand ShowClientInfoPageCommand { get; set; } =
        //    new RelayCommand (() => { Messenger.Default.Send("ClientPageDetails"); });


        //void ProcessMessage(string msg)
        //{
        //    switch (msg)
        //    {
        //        case "ClientPageDetails":
        //            CurrentPage = ApplicationPage.ClientPageDetails;
        //            break;

        //        case "AbonementControlPage":
        //            CurrentPage = ApplicationPage.ClientAccountControl;
        //            break;
        //    }
        //}



        public class Clients
        {
            ///Пока просто для отладки
            public Image Photo { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string FatherName { get; set; }

            public string TypeAbonement { get; set; }

            public string PhoneNumber { get; set; }

        }



        private ObservableCollection<Clients> clientsList = null;

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
        }

        /// <summary>
        /// абонемент клиенгта
        /// </summary>
       public class Abonement
        {
            public Abonement()
            {
                m_nNumber++;
            }

            public Abonement(string strStatus, string strService,string strTariff,int nAllCntTrainings, int nRemainedTrainings) : this()
            {
               
                m_strStatus = strStatus;
                m_strService = strService;
                m_strTariff = strTariff;
                m_nAllCntTrainings = nAllCntTrainings;
                m_nRemainedTrainings = nRemainedTrainings;
            }

            public static int m_nNumber { get; set; } = 0;//номер абонемента
            public string m_strStatus { get; set; } //статус абонемента
            public string m_strService { get; set; }//услуга
            public string m_strTariff { get; set; }//тариф
            public int m_nAllCntTrainings { get; set; }//куплено трениовок
            public int m_nRemainedTrainings { get; set; }//осталось трениовок
        }

        private ObservableCollection<Abonement> m_abonementList = null;

        public ObservableCollection<Abonement> GetAbonement
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
