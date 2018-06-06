using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.Utils;
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
    public class WorkingCabinetPageViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        /// Максимальное количество символов в номере абонемента
        /// </summary>
        private const int MAXCOUNTNUMBERABON = 4;

        /// <summary>
        /// Флаг того, что команда выполнялась из таблицы
        /// </summary>
        public bool IsFromClientTable { get; set; } = false;
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

        private Account _clientData;

        public Account ExistAccount { get; set; }

        /// <summary>
        /// Команда "сохранить изменения" после заполнения формы
        /// </summary>
        public RelayCommand<bool> SaveChangesCommand => new RelayCommand<bool>((obj) =>{
            {
                if (ExistAccount != null)
                {
                    CorrectExistData(ExistAccount);
                    _modelManager.CreateOrUpdateRecord<Account>(ExistAccount);
                    MessageBoxResult result = CustomMessageBox.Show(
                        "Обновление записи успешно завершено",
                        "Регистрация нового пользователя",
                        MessageBoxButton.OK, eMessageBoxIcons.eSucsess);
                    if (result == MessageBoxResult.OK)
                    {
                        Messenger.Default.Send("ClientPage");
                    }
                    return;
                }

                if (_clientData == null || !_clientData.bIsExistPerson)
                {
              
                    _clientData = new Account()
                    {
                        HumanFirstName = this.ClientFirstName,
                        HumanLastName = this.ClientLastName,
                        HumanFamilyName = this.ClientFamilyName,
                        HumanDateOfBirdth = this.ClientDateOfBirdth,
                        HumanGender = obj ? "Муж." : "Жен.",
                        HumanAdress = this.ClientAdress,
                        HumanPhoneNumber = this.ClientPhoneNumber,
                        HumanMail = this.ClientMail,
                        StrPathPhoto = this.StrPath,
                        HumanPasportDataSeries = this.ClientPasportDataSeries,
                        HumanPasportDataNumber = this.ClientPasportDataNumber,
                        HumanPasportDataIssuedBy = this.ClientPasportDataIssuedBy,
                        HumanPasportDatеOfIssue = this.ClientPasportDatеOfIssue,
                        HumanPhoto = Photo
                    };

                    _clientData = _modelManager.CheckRecord(_clientData);

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
                                this.StrPath = string.Empty;
                                UpdatePageFields(_clientData);// обновляем форму
                                return;

                            case MessageBoxResult.No:
                                ClearFields();
                                break;
                        }
                    }
                    else
                    {
                        _clientData = new Account()
                        {
                            HumanFirstName = this.ClientFirstName,
                            HumanFamilyName = this.ClientFamilyName,
                            HumanLastName = this.ClientLastName,
                            HumanDateOfBirdth = this.ClientDateOfBirdth,
                            HumanGender = obj ? "Муж." : "Жен.",
                            HumanAdress = this.ClientAdress,
                            HumanPhoneNumber = this.ClientPhoneNumber,
                            HumanMail = this.ClientMail,
                            StrPathPhoto = this.StrPath,
                            HumanPasportDataSeries = this.ClientPasportDataSeries,
                            HumanPasportDataNumber = this.ClientPasportDataNumber,
                            HumanPasportDataIssuedBy = this.ClientPasportDataIssuedBy,
                            HumanPasportDatеOfIssue = this.ClientPasportDatеOfIssue,
                            HumanPhoto = Photo
                        };

                        _modelManager.CreateOrUpdateRecord<Account>(_clientData);
                        CustomMessageBox.Show(
                            "Новая учетная запись успешно создана",
                            "Регистрация нового пользователя",
                            MessageBoxButton.OK, eMessageBoxIcons.eSucsess);
                        //ClearFields();
                     //   _clientData = null;
                    }
                }
                else
                {
                    CorrectExistData(_clientData);
                    //_clientData.HumanFirstName = this.ClientFirstName;
                    //_clientData.HumanFamilyName = this.ClientFamilyName;
                    //_clientData.HumanLastName = this.ClientLastName;
                    //_clientData.HumanDateOfBirdth = this.ClientDateOfBirdth;
                    //_clientData.HumanPhoneNumber = this.ClientPhoneNumber;
                    //_clientData.HumanPasportDataSeries = this.ClientPasportDataSeries;
                    //_clientData.HumanPasportDataNumber = this.ClientPasportDataNumber;
                    //_clientData.HumanPasportDataIssuedBy = this.ClientPasportDataIssuedBy;
                    //_clientData.HumanPasportDatеOfIssue = this.ClientPasportDatеOfIssue;
                    //_clientData.StrPathPhoto = this.StrPath;
                    //_clientData.HumanAdress = this.ClientAdress;
                    //_clientData.HumanMail = this.ClientMail;
                    _modelManager.CreateOrUpdateRecord<Account>(_clientData);
                    CustomMessageBox.Show(
                        "Обновление записи успешно завершено",
                        "Регистрация нового пользователя",
                        MessageBoxButton.OK, eMessageBoxIcons.eSucsess);
                    //ClearFields();
                  // _clientData = null;
                }
              
            }
            _clientData = null;

        }, (obj) =>  IsOk);



        void CorrectExistData(Account clientData)
        {
            clientData.HumanFirstName = this.ClientFirstName;
            clientData.HumanFamilyName = this.ClientFamilyName;
            clientData.HumanLastName = this.ClientLastName;
            clientData.HumanDateOfBirdth = this.ClientDateOfBirdth;
            clientData.HumanPhoneNumber = this.ClientPhoneNumber;
            clientData.HumanPasportDataSeries = this.ClientPasportDataSeries;
            clientData.HumanPasportDataNumber = this.ClientPasportDataNumber;
            clientData.HumanPasportDataIssuedBy = this.ClientPasportDataIssuedBy;
            clientData.HumanPasportDatеOfIssue = this.ClientPasportDatеOfIssue;
            clientData.StrPathPhoto = this.StrPath;
            clientData.HumanAdress = this.ClientAdress;
            clientData.HumanMail = this.ClientMail;
        }

        /// <summary>
        /// Команда поиска клиента по номеру абонемента (для проверки свойств необходимо задавать <bool>)
        /// </summary>
        public RelayCommand<bool> FindClientForNumberSubsription  => new RelayCommand<bool>((obj) =>
        {
            _clientData = _modelManager.FindPersonForNumberSubsription(Int32.Parse(NumberSubscription as string));

            
             if (_clientData != null)
             {
                 Messenger.Default.Send("RegisterNewClientPage");
                 UpdatePageFields(_clientData);

                 _modelManager.CreateOrUpdateRecord<Account>(_clientData);
             }
             else
             {
                 CustomMessageBox.Show(
                     $"Клиента с номером абонемента {NumberSubscription} не найдено",
                     "Регистрация нового пользователя",
                     MessageBoxButton.OK, eMessageBoxIcons.eWarning);
             }
        }, (obj) => NumberSubscription?.Length == MAXCOUNTNUMBERABON);

        /// <summary>
        /// Обновление полей
        /// </summary>
        /// <param name="clientData">данные клиента</param>
        public void UpdatePageFields(Account clientData)
        {
            if (clientData == null)
                return;
          
            this.ClientFirstName = clientData.HumanFirstName;
            this.ClientFamilyName = clientData.HumanFamilyName;
            this.ClientLastName = clientData.HumanLastName;
            this.ClientDateOfBirdth = clientData.HumanDateOfBirdth;
            this.ClientPhoneNumber = clientData.HumanPhoneNumber;
            this.ClientPasportDataSeries = clientData.HumanPasportDataSeries;
            this.ClientPasportDataNumber = clientData.HumanPasportDataNumber;
            this.ClientPasportDataIssuedBy = clientData.HumanPasportDataIssuedBy;
            this.ClientPasportDatеOfIssue = clientData.HumanPasportDatеOfIssue;
            this.StrPath = clientData.StrPathPhoto;
            this.ClientAdress = clientData.HumanAdress;
            this.ClientMail = clientData.HumanMail;
            Photo = clientData.HumanPhoto;
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
        public string StrPath
        {
            get { return (String.IsNullOrEmpty(strPath)) ? string.Empty : strPath; }
            private set { strPath = value; }
        }

        private string strPath;
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
                Photo = SqlTools.ConvertImageToByteArray(StrPath);
            }
        });

        /// <summary>
        /// Очистка фотографии работника
        /// </summary>
        public RelayCommand ClearPhotoCommand =>new RelayCommand(() =>
        {
            if (!string.IsNullOrEmpty(StrPath))
            {
                Photo = null;
                Visib = Visibility.Hidden;
            }
        });

        /// <summary>
        /// Свойство видимости кнопки очистки фотографии
        /// </summary>
        public Visibility Visib
        {
            get => !string.IsNullOrEmpty(StrPath) ? Visibility.Visible : Visibility.Hidden;
            private set { visibility = value; }
        }

        private Visibility visibility;

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

        #region Проверка заполнения полей
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

        public string ClientAdress { get; set; }
        //[DataTypeAttribute(DataType.EmailAddress, ErrorMessage = "dsfdsf")]
        public string ClientMail { get; set; }

        public byte[] Photo { get; set; }

        #region Поленомер абонемента
        /// <summary>
        /// Номер абонемента
        /// </summary>
        public string NumberSubscription { get; set; }
        #endregion


        #endregion

    }

}
