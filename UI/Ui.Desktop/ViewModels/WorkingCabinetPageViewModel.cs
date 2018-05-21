using System;
using System.Windows;
using FitnesClubCL;
using FitnesClubCL.Classes;
using FitnessClubMWWM.Ui.Desktop.Constants;
using FitnessClubMWWM.Ui.Desktop.Interfaces;
using FitnessClubMWWM.Ui.Desktop.Pages;
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
        public NewClientData ClientData { get; private set; } = new NewClientData();

        /// <summary>
        /// Команда "сохранить изменения" после заполнения формы
        /// </summary>
        public RelayCommand<Tuple<string, string, string, bool, string, string, string, Tuple<string, DateTime, string, DateTime>>> SaveChangesCommand => new RelayCommand<Tuple<string, string, string, bool, string, string, string, Tuple<string, DateTime, string, DateTime>>>((obj)=>{
            {
                //в случае если  все поля не заполнены
                if (obj == null)
                {
                    CustomMessageBox.Show("Заполнены не все обязательные поля!", "Регистрация нового клиента",
                        MessageBoxButton.OK, eMessageBoxIcons.eWarning);
                    return;
                }

                //Преобразуем данные с вьюшки
                var newClientData = ClientData;
                newClientData.ClientName = obj.Item1; //фамилия
                newClientData.ClientFamilyName = obj.Item2; //имя
                newClientData.ClientLastName = obj.Item3; //отчество
                newClientData.ClientGender = obj.Item4 ? "Муж." : "Жен."; //пол
                newClientData.ClientPhoneNumber = obj.Item5; //телефон
                newClientData.ClientPasportDataSeries = obj.Item6; //серия
                newClientData.ClientPasportDataNumber = obj.Item7; //номер
                newClientData.ClientPasportDataIssuedBy = obj.Rest.Item1; //выдан
                newClientData.ClientPasportDatеOfIssue = obj.Rest.Item2; //дата выдачи
                newClientData.ClientEmail = obj.Rest.Item3; //е-мейл
                newClientData.ClientDateOfBirdth = obj.Rest.Item4; //дата рождения
                newClientData.IsExistClient = false;
                if (!string.IsNullOrEmpty(StrPath)) // путь к фотке клиента
                {
                    newClientData.ClientPhotoPath = StrPath;
                }

                ClientData = newClientData;

                //В случае если не заполнена часть полей
                //if (ModelManager.GetInstance().CheckRequiredParams(ClientData))
                //{
                //    CustomMessageBox.Show("Заполнены не все обязательные поля!", "Регистрация нового клиента",
                //        MessageBoxButton.OK, eMessageBoxIcons.eWarning);
                //    return;
                //}

                newClientData = ClientData;
                ModelManager.GetInstance().RegisterNewClient(ref newClientData);
                //Проверить эту логику + в случае успеха вернуть номер абонемента и id-шник
                if (newClientData.IsExistClient)
                {
                    MessageBoxResult result = CustomMessageBox.Show(
                        "Данный клиент существует в БД! \n Желаете внести изменения в запись",
                        "Регистрация нового клиента", MessageBoxButton.YesNo, eMessageBoxIcons.eWarning);

                    if (result == MessageBoxResult.Yes)
                    {
                        ClientData = newClientData;
                        StrPath = ClientData.ClientPhotoPath;
                    }
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }

                }
            }

        });

        /// <summary>
        /// Путь к фотографии клиента
        /// </summary>
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
            get { return !string.IsNullOrEmpty(StrPath) ? Visibility.Visible : Visibility.Hidden; }
            private set { Visib = value; }
        }
    }
}
