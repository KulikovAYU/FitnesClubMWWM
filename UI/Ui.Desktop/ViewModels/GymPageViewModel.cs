using System;
using System.Collections.ObjectModel;
using System.Windows;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnesClubCL;
using FitnessClubMWWM.Ui.Desktop.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
   public class GymPageViewModel : ViewModelBase
    {
        public GymPageViewModel()
        {
            Gyms = ModelManager.GetReferenceData<Gym>();
        }
        /// <summary>
        /// Выбранный зал
        /// </summary>
        public Gym SelectedGym { get; set; }


        /// <summary>
        /// Имя
        /// </summary>
        public string GymName { get; set; } 

        /// <summary>
        /// Вместимость
        /// </summary>
        public string GymCapacity { get; set; }

        public RelayCommand GoHomeCommand => new RelayCommand(() => { Messenger.Default.Send("MainPage");});

        public RelayCommand ShowGymWindowsCommand => new RelayCommand(() => { (new GymWindow()).ShowDialog();  });

        public RelayCommand EditGymInfoCommand => new RelayCommand(() =>
        {
            if(SelectedGym == null)
                return;
            GymName = SelectedGym.GymName;
            GymCapacity = SelectedGym.GymCapacity.ToString();
            (new GymWindow()).ShowDialog();

        });


        public RelayCommand<Window> ApplyCommand => new RelayCommand<Window>((gym) =>
        {
            (gym as Window).Close();
            if (SelectedGym == null)
                SelectedGym = new Gym();
                
            SelectedGym.GymName = GymName;
            SelectedGym.GymCapacity = Int32.Parse(GymCapacity);
            ModelManager.GetInstance.CreateOrUpdateRecord(SelectedGym);
            GymName = String.Empty;
            GymCapacity = String.Empty;
        });

        public RelayCommand RemoveCommand => new RelayCommand(() => { ModelManager.GetInstance.RemoveItem<Gym>(SelectedGym); Gyms=ModelManager.GetReferenceData<Gym>(); });

        //public class Gym
        //{
        //    public Gym()
        //    {
        //        nNum = ++nUmber;
        //    }


        //    private static int nUmber=0;
        //    public int nNum { get;  set; }

        //    public string GymType { get; set; }

        //    public string GymCapacity { get; set; }

        //}





        /// <summary>
        /// получить список залов
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Gym> GetGyms() =>  ModelManager.GetReferenceData<Gym>();

        private ObservableCollection<Gym> _gyms;
        public ObservableCollection<Gym> Gyms {
            get
            {
                return _gyms;
            }
            set { _gyms = value; }
        }

    }
}

