﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnesClubCL;
using FitnessClubMWWM.Ui.Desktop.Constants;
using FitnessClubMWWM.Ui.Desktop.DataModels;
using FitnessClubMWWM.Ui.Desktop.Pages;
using FitnessClubMWWM.Ui.Desktop.Pages.Wind;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    /// <summary>
    /// Вью модель для страницы информации об абонементе
    /// </summary>
    public class AbonementInfoViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        /// Ссылка не текущий аккаунт посетителя
        /// </summary>
        public Account _Account { get; set; }

        /// <summary>
        /// Предстоящие тренировки с учетом наличия этого типа тренировок в абонементе
        /// </summary>
        public ObservableCollection<UpcomingTraining> ArrAvailableTrainings { get; set; }

        /// <summary>
        /// Номер абонемента
        /// </summary>
        private string _numberSubscription;
        /// <summary>
        /// Текущая тренировка в абонементе
        /// </summary>
        public ServicesInSubscription CurrentServiceInSubscription { get; set; }

        /// <summary>
        /// Ссылка на окно записи
        /// </summary>
        private CreateRecordWindow _recordWindow;

        /// <summary>
        /// Список предстоящих тренировок
        /// </summary>
        private ObservableCollection<UpcomingTraining> _arrUpcomingTraining;


        /// <summary>
        /// Список доступных тренировок
        /// </summary>
        private ObservableCollection<ServicesInSubscription> _arrServicesInSubscription;


        /// <summary>
        /// Выбранная тренировка
        /// </summary>
        public UpcomingTraining CurrentItem { get; set; }

        public string NumberSubscription
        {
            get => _Account?.Abonement.NumberSubscription.ToString();
            set => _numberSubscription = value;
        }

        public AbonementInfoViewModel()
        {
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }


       

        /// <summary>
        /// Список доступных услуг в абонементе
        /// </summary>
        public  ObservableCollection<ServicesInSubscription> ArrServicesInSubscription 
        {
            get
            {
                if (_Account?.Abonement != null)
                  return  _arrServicesInSubscription = ModelManager.GetInstance.GetServicesInSubscription(_Account);
                return null;
            }
            set => _arrServicesInSubscription = value;
        }

     
        /// <summary>
        /// Список предстоящих тренировок
        /// </summary>
        public  ObservableCollection<UpcomingTraining> ArrUpcomingTraining
        {
            get
            {
                if (_Account?.Abonement != null)
                    return _arrUpcomingTraining = ModelManager.GetInstance.GetUpcomingTraining(_Account);
               
                return null;
            }
            set => _arrUpcomingTraining = value;
        }

      
        /// <summary>
        /// Записаться на тренировку
        /// </summary>
        public RelayCommand<ServicesInSubscription> CreateRecordCommand => new RelayCommand<ServicesInSubscription>((selectedItem) =>
        {
            CurrentServiceInSubscription = selectedItem as ServicesInSubscription;
            _recordWindow = new CreateRecordWindow();
            ArrAvailableTrainings = ModelManager.GetInstance.GetAvailableTrainings(CurrentServiceInSubscription);
            _recordWindow.ShowDialog();
        });


        /// <summary>
        /// Создание записи на тренировку (через DataGrid)
        /// </summary>
        public RelayCommand CreateVisitCommand => new RelayCommand(
            () =>
            {
                //1. Создали предварительную регистрацию
                ModelManager.GetInstance.CreatePriorRegistration(_Account,CurrentServiceInSubscription, CurrentItem);
                //2. Обновили поле Доступные тренировки
                ArrServicesInSubscription = ModelManager.GetInstance.GetServicesInSubscription(_Account);
                //3. Обновили список записей (если останется время отфильтровать все тренировки, которые есть уже в записи)
                ArrUpcomingTraining = ModelManager.GetInstance.GetUpcomingTraining(_Account);
                //4. Закроем диалог
                _recordWindow.Close();
            }, ModelManager.GetInstance.CheckTrainingOnAvailable(CurrentItem)  );

        /// <summary>
        /// Отметка о посещении тренировки (пришел)
        /// </summary>
        public RelayCommand<UpcomingTraining> FixTheVisitCommand => new RelayCommand<UpcomingTraining>((upcTraining) =>
            {
                ModelManager.GetInstance.FixTheVisit(_Account,upcTraining as UpcomingTraining);
                ArrUpcomingTraining = ModelManager.GetInstance.GetUpcomingTraining(_Account);

            });

        /// <summary>
        /// Отказ о посещении тренировки (отмена тренировки)
        /// </summary>
        public RelayCommand<UpcomingTraining> RefusalOfVisitCommand => new RelayCommand<UpcomingTraining>(
            (upcTraining) =>
            {

                ModelManager.GetInstance.RefusalOfVisit(_Account, upcTraining);
                //2. Обновили поле Доступные тренировки
                ArrServicesInSubscription = ModelManager.GetInstance.GetServicesInSubscription(_Account);
                //3. Обновим список записей
                ArrUpcomingTraining = ModelManager.GetInstance.GetUpcomingTraining(_Account);

            });

        // <summary>
        // Текущая страница приложения
        // </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.AbonementInfoDetails; // ApplicationPage.Login;  ApplicationPage.MainPage

        /// <summary>
        /// Страницы
        /// </summary>
        /// <param name="msg"></param>
        void ProcessMessage(string msg)
        {
            switch (msg)
            {

                case "ShowAbonementInfo":
                    CurrentPage = ApplicationPage.AbonementInfoDetails;
                    break;
                case "ShowTrainingAndServiceListCommand":
                    CurrentPage = ApplicationPage.TrainingAndServiceList;
                    break;
                case "LongAbonement":
                    CurrentPage = ApplicationPage.LongAbonement;
                    break;
                case "PayServicesAndTrainings":
                    CurrentPage = ApplicationPage.PayServicesAndTrainings;
                    break;
            }
        }

        public RelayCommand ShowAbonementInfoCommand => new RelayCommand(() => Messenger.Default.Send("ShowAbonementInfo"));
        public RelayCommand ShowTrainingAndServiceListCommand => new RelayCommand(() => Messenger.Default.Send("ShowTrainingAndServiceListCommand"));

        /// <summary>
        /// Крманда продать абонемент (из таблицы)
        /// </summary>
        public RelayCommand<Account> SellTrainingOrServiceCommand => new RelayCommand<Account>((item) => {
            Messenger.Default.Send("PayServicesAndTrainings");
        });

        /// <summary>
        /// Команда продлить абонемент (показ страницы)
        /// </summary>
        public RelayCommand<bool> ShowLongAbonementPageCommand => new RelayCommand<bool>((_)=>
        {
            if (_Account == null)
                return;
            Messenger.Default.Send("LongAbonement");
          
        },_=> _Account?.Abonement?.AbonmentStatus?.StatusName == "Активен");

        /// <summary>
        /// Команда продлить абонемент
        /// </summary>
        public RelayCommand<bool> ShowLongAbonemenCommand => new RelayCommand<bool>((_) =>
        {
            if (_Account == null)
                return;
           
            ModelManager.GetInstance.LongAbonement(_Account.Abonement, TimeToLong.Value);
            CustomMessageBox.Show(
                "Обновление записи успешно завершено",
                "Продление абонемента",
                MessageBoxButton.OK, eMessageBoxIcons.eSucsess);
        }, _ => IsOk);



        /// <summary>
        /// Активировать абонемент
        /// </summary>
        public RelayCommand<bool> ActivateAbonementCommand
        {
            get
            {
                return new RelayCommand<bool>((obj) =>
                    {
                        if (_Account == null)
                            return;
                       
                        ModelManager.GetInstance.ActivateAbonement(_Account.Abonement);
                    }, (obj)=> isFreeze);
            }
        }


        /// <summary>
        /// Заморозить абонемент
        /// </summary>
        public RelayCommand<bool> FreezeAbonementCommand
        {
            get
            {
                return new RelayCommand<bool>((obj) =>
                {
                    if (_Account == null)
                        return;

                    ModelManager.GetInstance.FreezeAbonement(_Account.Abonement);
                    ActivateAbonementCommand.RaiseCanExecuteChanged();
                    CustomMessageBox.Show(
                        "Аккаунт заморожен",
                        "Заморозка аккаунта",
                        MessageBoxButton.OK, eMessageBoxIcons.eFreeze);
                    SimpleIoc.Default.GetInstance<ClientsPageViewModel>().GetClientsList();
                }, (obj) => isActive);
            }
        }

        public bool isFreeze => _Account?.Abonement?.AbonmentStatus?.StatusName == "Заморожен";
        public bool isActive => _Account?.Abonement?.AbonmentStatus?.StatusName == "Активен";
        /// <summary>
        /// Команда закрытия диалогового окна
        /// </summary>
        public RelayCommand<Window> CloseCommand { get; set; } =
            new RelayCommand<Window>((window) => { window?.Close(); });


        public int ResizeBorder { get; set; } = 6;
      

        /// <summary>
        /// Устанавливает ссылку на текущий аакаунт
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="account"></param>
        public void SetData<T>(T account)
        {
            _Account = account as Account;
        }

        /// <summary>
        /// Проверка на корректность введенных пользователем данных
        /// </summary>
        /// <param name="propertyName">Имя свойства</param>
        /// <returns></returns>
        public string this[string propertyName]
        {
            get
            {
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

            //Reflection
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).
                Where(prop => prop.IsDefined(typeof(RequiredAttribute), true) || prop.IsDefined(typeof(MaxLengthAttribute), true)
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
                        if (string.IsNullOrEmpty(currentValue?.ToString() ?? string.Empty))
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


            ShowLongAbonemenCommand.RaiseCanExecuteChanged();
            IsOk = !HasErrors;
        }


        public string Error => String.Empty;


        /// <summary>
        /// Коллекция ошибок
        /// </summary>
        private Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();
        public bool HasErrors => Errors.Any();
        public bool IsOk { get; private set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Дата должна быть введена")]
        public DateTime? TimeToLong { get; set; } = DateTime.Now;


    }
}
