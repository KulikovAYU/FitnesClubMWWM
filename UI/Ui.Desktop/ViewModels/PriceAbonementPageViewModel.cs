using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FitnessClubMWWM.Ui.Desktop.Pages;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
   public class PriceAbonementPageViewModel : ViewModelBase
    {
        public RelayCommand GoHomeCommand => new RelayCommand(() => { Messenger.Default.Send("MainPage"); });

        public RelayCommand ShowAddNewTrainingCommand => new RelayCommand(() =>
        {
            (new AddNewTrainingWindow()).ShowDialog();});


        public class Training
        {
            public int nCount { get; set; }
            public string TrainingService { get; set; }
            public string TrainingTariff { get; set; }
            public double TrainingCost { get; set; }
        }


        private ObservableCollection<Training> trainingList = null;

        public ObservableCollection<Training> TrainingsList
        {
            get
            {
                if (trainingList != null)
                    return trainingList;

                trainingList = new ObservableCollection<Training>
                { 
                   new Training() { nCount=10, TrainingService = "Групповое занятие в фитнес-зале", TrainingTariff = "Утренний", TrainingCost = 200.0},
                   new Training() { nCount=20, TrainingService = "Групповое занятие в фитнес-зале", TrainingTariff = "Утренний", TrainingCost = 350.0},
                   new Training() { nCount=25, TrainingService = "Групповое занятие в фитнес-зале", TrainingTariff = "Утренний", TrainingCost = 400.0}
                };
                return trainingList;
            }
        }
    }
}
