using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FitnessClubMWWM.Ui.Desktop.Pages;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    public class StaffPaymentViewModel : ViewModelBase
    {
        public RelayCommand GoHomeCommand => new RelayCommand(() => { Messenger.Default.Send("MainPage"); });

        public RelayCommand ShowSalariesEmployeesWindowCommand =>
            new RelayCommand(() => (new SalariesEmployeesWindow()).ShowDialog());

        /// <summary>
        /// зарплата (для отладки)
        /// </summary>
        public class Salary
        {
            public string Position { get; set; }
          
            public string Payment { get; set; }
        }

        private ObservableCollection<Salary> arrSalaryList = null;

        public ObservableCollection<Salary> SalaryList {
            get
            {
                if (arrSalaryList != null)
                    return arrSalaryList;

                arrSalaryList = new ObservableCollection<Salary>
                {
                    new Salary() {Position = "Руководитель", Payment = "50000"},
                    new Salary() {Position = "Менеджер", Payment = "30000"},
                    new Salary() {Position = "Фитнес - инструктор", Payment = "35000"},
                    new Salary() {Position = "Уборщик", Payment = "15000"},
                };
                return arrSalaryList;
            }
        }

    }
}
