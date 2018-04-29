using System;
using System.Collections.ObjectModel;
using System.Globalization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    public class ScheduleViewModel : ViewModelBase
    {

        public RelayCommand GoHomeCommand => new RelayCommand(() => { Messenger.Default.Send("MainPage"); });

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

                m_trainList = new ObservableCollection<Training>();

                m_trainList.Add(new Training { Name = "Cycle", NameGym = "Зал N3" });
                m_trainList.Add(new Training { Name = "PowerPump", NameGym = "Зал N5" });
                m_trainList.Add(new Training { Name = "Кенезис", NameGym = "Зал N1" });
                m_trainList.Add(new Training { Name = "Круговая", NameGym = "Зал N10" });
                return m_trainList;
            }
          

        }
    }
}
