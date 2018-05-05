using System.Collections.ObjectModel;
using FitnessClubMWWM.Ui.Desktop.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
   public class GymPageViewModel : ViewModelBase
    {

        public RelayCommand GoHomeCommand => new RelayCommand(() => { Messenger.Default.Send("MainPage"); });

        public RelayCommand ShowGymWindowsCommand => new RelayCommand(()=> (new GymWindow()).ShowDialog());

        public class Gym
        {
            public Gym()
            {
                nNum = ++nUmber;
            }


            private static int nUmber=0;
            public int nNum { get;  set; }

            public string GymType { get; set; }

            public string GymCapacity { get; set; }

        }

     

        private ObservableCollection<Gym> gymList = null;

        public ObservableCollection<Gym> GymList
        {
            get
            {
                if (gymList != null)
                    return gymList;

            
                gymList = new ObservableCollection<Gym>
                {
                    new Gym()
                    {
                        GymType="Фитнес-зал",
                        GymCapacity = "20 человек"
                    },

                    new Gym()
                    {
                        GymType="Фитнес-зал",
                        GymCapacity = "15 человек"
                    },
                    new Gym()
                    {
                        GymType="Cycle Stutio",
                        GymCapacity = "15 человек"
                    },
                    new Gym()
                    {
                        GymType="Студия йоги",
                        GymCapacity = "10 человек"
                    },
                    new Gym()
                    {
                        GymType="Тренажерный зал",
                        GymCapacity = "50 человек"
                    },
                };
                return gymList;
            }
        }

    }
}

