using System.Collections.ObjectModel;
using System.Windows.Controls;
using GalaSoft.MvvmLight;

namespace FitnessClubMWWM.Ui.Desktop
{
    public class ClientsPageViewModel : ViewModelBase
    {
        public DataGridLength FirstColumnWidth { get; set; } = new DataGridLength(50);

        public DataGridLength FamilyWidth { get; set; } = 150.0;




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

                clientsList = new ObservableCollection<Clients>();
                clientsList.Add(new Clients(){ Photo = null, LastName = "Куликов", FirstName = "Антон", FatherName = "Юрьевич", TypeAbonement="Vip",PhoneNumber = "8-920-672-0-68" });
                clientsList.Add(new Clients() { Photo = null, LastName = "РПОРрыов", FirstName = "пав", FatherName = "пав", TypeAbonement = "Норма", PhoneNumber = "8-920-672-0-32" });
                return clientsList;
            }
        }
    }
}
