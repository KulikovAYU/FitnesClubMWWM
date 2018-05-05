using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FitnessClubMWWM.Ui.Desktop.Pages;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    public class ScheduleViewModel : ViewModelBase
    {

        public RelayCommand GoHomeCommand => new RelayCommand(() => { Messenger.Default.Send("MainPage"); });

        /// <summary>
        /// Закрытие (нажатие на крестик)
        /// </summary>
        public RelayCommand<Window> CloseCommand => new RelayCommand<Window>((window) => { window?.Close(); });

        public RelayCommand ShowNewEntryInScheduleWindowCommand => new RelayCommand(() =>
        {
            (new NewEntryInScheduleWindow()).ShowDialog(); });


        ///чисто для разметки
        public class Training
        {
            public string Name { get; set; }
            public string Time
            {
                get => DateTime.Now.ToString(CultureInfo.InvariantCulture);
                set => Time = value ?? throw new ArgumentNullException(nameof(value));
            }

            public string NameGym { get; set; }
        }

        private ObservableCollection<Training> m_trainList = null;

        public ObservableCollection<Training> GetTrainings
        {
            get
            {
                if (m_trainList != null)
                    return m_trainList;

                m_trainList = new ObservableCollection<Training>
                {
                    new Training {Name = "Cycle", NameGym = "Зал N3"},
                    new Training {Name = "PowerPump", NameGym = "Зал N5"},
                    new Training {Name = "Кенезис", NameGym = "Зал N1"},
                    new Training {Name = "Круговая", NameGym = "Зал N10"}
                };

                return m_trainList;
            }
          

        }
    }
}
