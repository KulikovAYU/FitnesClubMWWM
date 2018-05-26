using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using FC_EMDB.Classes;
using FitnesClubCL;
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
    public class WorkingCabinetPageViewModel : ViewModelBase, IDataErrorInfo, IClient
    {
        private readonly ModelManager _modelManager;
        public WorkingCabinetPageViewModel()
        {
            Visib = Visibility.Hidden;
            _modelManager = ModelManager.GetInstance;

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
        private NewClientData _clientData;
       
    
        /// <summary>
        /// Команда "сохранить изменения" после заполнения формы
        /// </summary>
        public RelayCommand<bool> SaveChangesCommand => new RelayCommand<bool>((obj) =>{
            {
                if (_clientData == null || !_clientData.IsExistPerson )
                {
                    _clientData = new NewClientData()
                    {
                        PersonRole = "Клиент",
                        PersonFirstName = ClientFirstName,
                        PersonLastName = ClientLastName,
                        PersonFamilyName = ClientFamilyName,
                        PersonDateOfBirdth = ClientDateOfBirdth,
                        PersonGender = obj ? "Муж." : "Жен.",
                        PersonAdress = ClientAdress,
                        PersonPhoneNumber = ClientPhoneNumber,
                        PersonMail = ClientMail,
                        PathPersonPhoto = StrPath,
                        ClientPasportDataSeries = this.ClientPasportDataSeries,
                        ClientPasportDataNumber = this.ClientPasportDataNumber,
                        ClientPasportDataIssuedBy = this.ClientPasportDataIssuedBy,
                        ClientPasportDatеOfIssue = this.ClientPasportDatеOfIssue
                    };
                    _modelManager.CheckRecord(ref _clientData);

                    // В случае если запись в БД существует
                    if (_clientData != null)
                    {
                        MessageBoxResult result = CustomMessageBox.Show(
                            "Данный клиент зарегистрирован в системе.\nЗагрузить существующие данные для\nдальнейшего внесения изменений?",
                            "Регистрация нового пользователя",
                            MessageBoxButton.YesNo, eMessageBoxIcons.eAsk);

                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                UpdatePageFields(_clientData);// обновляем форму
                                return;

                            case MessageBoxResult.No:
                                ClearFields();
                                break;
                        }
                    }
                    else
                    {
                        _clientData = new NewClientData(true)
                        {
                            PersonRole = "Клиент",
                            PersonFirstName = ClientFirstName,
                            PersonLastName = ClientLastName,
                            PersonFamilyName = ClientFamilyName,
                            PersonDateOfBirdth = ClientDateOfBirdth,
                            PersonGender = obj ? "Муж." : "Жен.",
                            PersonAdress = ClientAdress,
                            PersonPhoneNumber = ClientPhoneNumber,
                            PersonMail = ClientMail,
                            PathPersonPhoto = StrPath,
                            ClientPasportDataSeries = this.ClientPasportDataSeries,
                            ClientPasportDataNumber = this.ClientPasportDataNumber,
                            ClientPasportDataIssuedBy = this.ClientPasportDataIssuedBy,
                            ClientPasportDatеOfIssue = this.ClientPasportDatеOfIssue
                            
                        };
                        _modelManager.CreateOrUpdateRecord<NewClientData>(_clientData);
                        CustomMessageBox.Show(
                            "Новая учетная запись успешно создана",
                            "Регистрация нового пользователя",
                            MessageBoxButton.OK, eMessageBoxIcons.eSucsess);
                        ClearFields();
                        _clientData = null;
                    }
                }
                else
                {
                    _clientData.PersonFirstName = ClientFirstName;
                    _clientData.PersonFamilyName = ClientFamilyName;
                    _clientData.PersonLastName = ClientLastName;
                    _clientData.PersonDateOfBirdth = ClientDateOfBirdth;
                    _clientData.PersonPhoneNumber = ClientPhoneNumber;
                    _clientData.ClientPasportDataSeries = ClientPasportDataSeries;
                    _clientData.ClientPasportDataNumber = ClientPasportDataNumber;
                    _clientData.ClientPasportDataIssuedBy = ClientPasportDataIssuedBy;
                    _clientData.ClientPasportDatеOfIssue = ClientPasportDatеOfIssue;
                    _clientData.PathPersonPhoto = StrPath;
                    _clientData.PersonAdress = ClientAdress;
                    _clientData.PersonMail = ClientMail;
                    _modelManager.CreateOrUpdateRecord<NewClientData>(_clientData);
                    CustomMessageBox.Show(
                        "Обновление записи успешно завершено",
                        "Регистрация нового пользователя",
                        MessageBoxButton.OK, eMessageBoxIcons.eSucsess);
                    ClearFields();
                   _clientData = null;
                }
            }
        

        }, (obj) =>  IsOk);

        /// <summary>
        /// Обновление полей
        /// </summary>
        /// <param name="clientData">данные клиента</param>
        private void UpdatePageFields(NewClientData clientData)
        {
            ClientFirstName = clientData.PersonFirstName;
            ClientFamilyName = clientData.PersonFamilyName;
            ClientLastName = clientData.PersonLastName;
            ClientDateOfBirdth = clientData.PersonDateOfBirdth;
            ClientPhoneNumber = clientData.PersonPhoneNumber;
            ClientPasportDataSeries = clientData.ClientPasportDataSeries;
            ClientPasportDataNumber = clientData.ClientPasportDataNumber;
            ClientPasportDataIssuedBy = clientData.ClientPasportDataIssuedBy;
            ClientPasportDatеOfIssue = clientData.ClientPasportDatеOfIssue;
            StrPath = clientData.PathPersonPhoto;
            ClientAdress = clientData.PersonAdress;
            ClientMail = clientData.PersonMail;
        }

        /// <summary>
        /// Метод чистит поля формы
        /// </summary>
        private void ClearFields()
        {
            ClientFirstName  = string.Empty;
            ClientFamilyName  = string.Empty;
            ClientLastName  = string.Empty;
            ClientDateOfBirdth  = null;
            ClientPhoneNumber = string.Empty;
            ClientPasportDataSeries = string.Empty;
            ClientPasportDataNumber  = string.Empty;
            ClientPasportDataIssuedBy  = string.Empty;
            ClientPasportDatеOfIssue = null;
            StrPath = string.Empty;
            ClientAdress = string.Empty;
            ClientMail = string.Empty;
        }

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
            if (string.IsNullOrEmpty(StrPath = string.Empty) && service.FilePath != null)
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

        /// <summary>
        /// Коллекция ошибок
        /// </summary>
        private Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Проверка на корректность введенных пользователем данных
        /// </summary>
        /// <param name="propertyName">Имя свойства</param>
        /// <returns></returns>
        public string this[string propertyName]
        {
            get
            {
                #region Второй способ

                //switch (propertyName)
                //{
                //    case nameof(ClientFirstName):
                //        if (string.IsNullOrEmpty(ClientFirstName))
                //        {
                //            return "Имя клиента должно быть определено";
                //        }
                //        break;
                //    case nameof(ClientLastName):
                //        if (string.IsNullOrEmpty(ClientLastName))
                //        {
                //            return "Отчество клиента должно быть определено";
                //        }
                //        break;
                //    case nameof(ClientFamilyName):
                //        if (string.IsNullOrEmpty(ClientFamilyName))
                //        {
                //            return "Фамилия клиента должна быть определена";
                //        }
                //        break;
                //}
                //return string.Empty;

                //Trace.TraceInformation(propertyName);

                #endregion
                CollectErrors();
                return Errors.ContainsKey(propertyName) ? Errors[propertyName] : String.Empty;
            }
        }

        /// <summary>
        /// Метод проверяет валидность заполнения полей
        /// </summary>
        private void CollectErrors()
        {
            Errors.Clear();

            #region Третий способ
            //if (string.IsNullOrEmpty(ClientFirstName))
            //{
            //   Errors.Add(nameof(ClientFirstName), "Имя клиента должно быть заполнено");
            //}
            //if (string.IsNullOrEmpty(ClientFamilyName))
            //{
            //    Errors.Add(nameof(ClientFamilyName), "Фамилия клиента должна быть заполенна");
            //}
            //if (string.IsNullOrEmpty(ClientLastName))
            //{
            //    Errors.Add(nameof(ClientLastName), "Отчество клиента должно быть заполнено");
            //}
            //if (string.IsNullOrEmpty(ClientPhoneNumber))
            //{
            //    Errors.Add(nameof(ClientPhoneNumber), "Телефон клиента должен быть заполнен");
            //}
            //if (string.IsNullOrEmpty(ClientPasportDataSeries))
            //{
            //    Errors.Add(nameof(ClientPasportDataSeries), "Серия паспорта должна быть заполнена");
            //}
            //if (string.IsNullOrEmpty(ClientPasportDataNumber))
            //{
            //    Errors.Add(nameof(ClientPasportDataNumber), "Номер паспорта должна быть заполнен");
            //}
            //if (string.IsNullOrEmpty(ClientPasportDataIssuedBy))
            //{
            //    Errors.Add(nameof(ClientPasportDataIssuedBy), "Место выдачи паспорта должно быть заполнено");
            //}
            //if (ClientDateOfBirdth == null)
            //{
            //    Errors.Add(nameof(ClientDateOfBirdth), "Дата рождения должна быть заполнена");
            //}
            //if (ClientPasportDatеOfIssue == null)
            //{
            //    Errors.Add(nameof(ClientPasportDatеOfIssue), "Дата выдачи паспотра должна быть заполнена");
            //}


            #endregion

            //Reflection
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).
                Where(prop =>prop.IsDefined(typeof(RequiredAttribute), true) || prop.IsDefined(typeof(MaxLengthAttribute),true) 
                                                                             || prop.IsDefined(typeof(MinLengthAttribute), true)
                                                                             || prop.IsDefined(typeof(DataTypeAttribute), true)
                                                                             ).ToList();
            properties.ForEach(
                prop =>
                {
                    var currentValue = prop.GetValue(this); //content of the property
                    var requiredAttr = prop.GetCustomAttribute<RequiredAttribute>();
                    var maxLenAtr = prop.GetCustomAttribute<MaxLengthAttribute>();
                    var minLenAtr = prop.GetCustomAttribute<MinLengthAttribute>();
                    var dataTypeAtr = prop.GetCustomAttribute<DataTypeAttribute>();
                    if (requiredAttr != null)
                    {
                        if (string.IsNullOrEmpty(currentValue?.ToString()?? string.Empty))
                        {
                            Errors.Add(prop.Name, requiredAttr.ErrorMessage);
                        }
                    }
                    if (maxLenAtr != null)
                    {
                        if ((currentValue?.ToString() ?? string.Empty).Length > maxLenAtr.Length)
                        {
                            Errors.Add(prop.Name, maxLenAtr.ErrorMessage);
                        }
                    }
                    if (dataTypeAtr != null)
                    {
                        if (currentValue?.GetType() != dataTypeAtr.GetType())
                        {
                            Errors.Add(prop.Name, dataTypeAtr.ErrorMessage);
                        }
                    }

                    if (minLenAtr != null)
                    {
                        if ((currentValue?.ToString() ?? string.Empty).Length < minLenAtr.Length)
                        {
                            Errors.Add(prop.Name, minLenAtr.ErrorMessage);
                        }
                    }
                }
            );


            SaveChangesCommand.RaiseCanExecuteChanged();
            IsOk = !HasErrors;
        }

        public string Error =>String.Empty;
        public bool HasErrors => Errors.Any();
        public bool IsOk { get; private set; }


        /// <summary>
        /// Личные данные посетителя
        /// </summary>
        // [Required(AllowEmptyStrings = false, ErrorMessage = "Имя клиента должно быть заполнено")]
        [MaxLength(20, ErrorMessage = "Имя допускает не более 20 символов")]
        [MinLength(2, ErrorMessage = "Минимальное имя не допускает меннее 2 символов")]
        public string ClientFirstName { get; set; } = "Анастасия";

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия клиента должна быть заполнена")]
        [MaxLength(30, ErrorMessage = "Фамилия допускает не более 30 символов")]
        [MinLength(2, ErrorMessage = "Минимальная фамилия не допускает меннее 2 символов")]
        public string ClientFamilyName { get; set; } = "Смирнова";

        // [Required(AllowEmptyStrings = false, ErrorMessage = "Отчество клиента должно быть заполнено")]
        [MaxLength(20, ErrorMessage = "Отчество допускает не более 20 символов")]
        [MinLength(2, ErrorMessage = "Минимальное отчество не допускает меннее 2 символов")]
        public string ClientLastName { get; set; } = "Николаевна";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Дата рождения должна быть заполнена")]
        public DateTime? ClientDateOfBirdth { get; set; } = DateTime.Parse("27.05.1989");

        [Required(AllowEmptyStrings = false, ErrorMessage = "Телефон клиента должен быть заполнен")]
        public string ClientPhoneNumber { get; set; } = "8-920-672-00-68";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Серия паспорта должна быть заполнена")]
        [MaxLength(4, ErrorMessage = "Серия паспорта допускает не более 4 символов")]
        public string ClientPasportDataSeries { get; set; } = "2409";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Номер паспорта должна быть заполнен")]
        [MaxLength(6, ErrorMessage = "Номер паспорта допускает не более 6 символов")]
        public string ClientPasportDataNumber { get; set; } = "460870";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Место выдачи паспорта должно быть заполнено")]
        public string ClientPasportDataIssuedBy { get; set; } = "ОУФМС РОССИИ";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Дата выдачи паспорта должна быть заполнена")]
        public DateTime? ClientPasportDatеOfIssue { get; set; } = DateTime.Parse("04.05.2009");


        public int NumberSubscription { get; set; }
        public string ClientGender { get; set; }
        public string ClientAdress { get; set; }
        //[DataTypeAttribute(DataType.EmailAddress, ErrorMessage = "dsfdsf")]
        public string ClientMail { get; set; }
        public byte[] ClientPhoto { get; set; }
    }

}
