using System;
using FitnesClubCL.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{

    /// <summary>
    /// Зарегистрировать нового клиента
    /// </summary>
    public class WorkingCabinetPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Команда выхода на главную страницу
        /// </summary>
        public RelayCommand GoHomeCommand => new RelayCommand(() => { Messenger.Default.Send("MainPage"); });

        /// <summary>
        /// Команда зарегистрировать нового клиента
        /// </summary>
        public RelayCommand RegisterNewClientCommand => new RelayCommand(() => { Messenger.Default.Send("RegisterNewClientPage"); });

        /// <summary>
        /// Данные нового зарегистрированного клиента
        /// </summary>
        public NewClientData ClientData { get; set; }

        /// <summary>
        /// Команда "сохранить изменения" после заполнения формы
        /// </summary>
        public RelayCommand<Tuple<string, string, string>> SaveChanges=>new RelayCommand<Tuple<string, string, string>>((obj)=>{
        {
            //пока для проверки потом поправить
            var newClientData = ClientData;
            newClientData.ClientName = obj.Item1;
            newClientData.ClientLastName = obj.Item2;
            newClientData.ClientFamily = obj.Item3;
        }
        });
    }
}
