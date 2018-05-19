using System;
using System.Windows;
using FitnesClubCL;
using FitnesClubCL.Classes;
using FitnessClubMWWM.Ui.Desktop.Interfaces;
using FitnessClubMWWM.Ui.Desktop.UserControls;
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

        public WorkingCabinetPageViewModel()
        {
            Visib = Visibility.Hidden;
        }
        /// <summary>
        /// Команда выхода на главную страницу
        /// </summary>
        public RelayCommand GoHomeCommand => new RelayCommand(() => { Messenger.Default.Send("MainPage");
            StrPath = string.Empty;
            Visib = Visibility.Hidden;
        });

        /// <summary>
        /// Команда зарегистрировать нового клиента
        /// </summary>
        public RelayCommand RegisterNewClientCommand => new RelayCommand(() => { Messenger.Default.Send("RegisterNewClientPage"); });

        /// <summary>
        /// Данные нового зарегистрированного клиента
        /// </summary>
        public NewClientData ClientData { get; private set; }

        /// <summary>
        /// Команда "сохранить изменения" после заполнения формы
        /// </summary>
        public RelayCommand<Tuple<string, string, string, bool, string, string, string, Tuple<string, DateTime>>> SaveChangesCommand => new RelayCommand<Tuple<string, string, string, bool, string, string, string, Tuple<string, DateTime>>>((obj)=>{
        {
            var newClientData = ClientData;
            newClientData.ClientName = obj.Item1; //фамилия
            newClientData.ClientFamily = obj.Item2;//имя
            newClientData.ClientLastName = obj.Item3;//отчество
            newClientData.ClientGender = obj.Item4 ? "Муж." : "Жен.";//пол
            newClientData.ClientPhoneNumber = obj.Item5; //телефон
            newClientData.ClientPasportDataSeries = obj.Item6;//серия
            newClientData.ClientPasportDataNumber = obj.Item7;//номер
            newClientData.ClientPasportDataIssuedBy = obj.Rest.Item1;//выдан
            newClientData.ClientPasportDatеOfIssue = obj.Rest.Item2;//дата выдачи
            newClientData.strImagePath = StrPath;
            ClientData = newClientData;
            ModelManager.GetInstance().RegisterNewClient(ClientData);
        }
        });


        public string StrPath { get;private set; }

        /// <summary>
        /// Загрузить фото пользователя
        /// </summary>
        public RelayCommand LoadClientPhotoCommand => new RelayCommand(() =>
        {
            IDialogService service = DefaultDialogService.CreateDialogService;
            service.OpenFileDialog(string.Empty, "Image Files(*.BMP; *.JPG; *.GIF; *.PNG) | *.BMP;*.JPG;*.PNG;");
            if (string.IsNullOrEmpty(StrPath))
            {
                StrPath = service.FilePath;
                Visib = Visibility.Visible;
            }
        });

        /// <summary>
        /// Очистка фотографии работника
        /// </summary>
        public RelayCommand ClearPhotoCommand =>new RelayCommand(() =>
        {
            if (!string.IsNullOrEmpty(StrPath))
            {
                StrPath = string.Empty;
                Visib = Visibility.Hidden;
            }
        });

        /// <summary>
        /// Свойство видимости кнопки очистки фотографии
        /// </summary>
        public Visibility Visib
        {
            get;
            private set;
        }
    }
}
