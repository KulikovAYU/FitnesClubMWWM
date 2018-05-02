using System;
using System.Collections.ObjectModel;
using FitnessClubMWWM.Ui.Desktop.Pages.Wind;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
   public class PriceAbonementPageViewModel : ViewModelBase
    {
        public RelayCommand GoHomeCommand => new RelayCommand(() => { Messenger.Default.Send("MainPage"); });

        public RelayCommand ShowAddNewTrainingCommand => new RelayCommand(() =>
        {
            (new AddNewTrainingWindow()).ShowDialog();});


        public class Training<T>
        {
            public T Name { get; set; }
            public T Type { get; set; }
            public T Duration { get; set; }
        }


        private ObservableCollection<Training<string>> trainingList = null;

        public ObservableCollection<Training<string>> TrainingsList
        {
            get
            {
                if (trainingList != null)
                    return trainingList;

                trainingList = new ObservableCollection<Training<string>>
                { 
                   new Training<string>() { Name = "Interval", Type = "Активный фитнес", Duration = "60 мин"},
                   new Training<string>() { Name = "Step", Type = "Активный фитнес", Duration = "60 мин"},
                   new Training<string>() { Name = "LatinaDance", Type = "Танцевальная тренировка", Duration = "60 мин"}
                };
                return trainingList;
            }
        }
    }
}
