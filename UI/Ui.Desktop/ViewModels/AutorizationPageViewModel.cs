using System.Windows;
using CommonServiceLocator;
using FitnesClubCL;
using FitnesClubCL.Classes;
using FitnessClubMWWM.Ui.Desktop.Constants;
using FitnessClubMWWM.Ui.Desktop.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    /// <summary>
    /// Данная вью модель управляет авторизацией пользователя
    /// </summary>
   public class AutorizationPageViewModel : ViewModelBase, ISystemUser
    {
       
        public AutorizationPageViewModel()
        {
            
        }
        public RelayCommand Autentification => new RelayCommand(Autorization);

        /// <summary>
        /// Метод авторизации
        /// </summary>
        void Autorization()
        {
            _commandStarted = true;// TODO: пока сделал костыль. Если будет время, то подумать

            if (UserName == string.Empty && PasswordString == string.Empty)
            {
                MessageBoxResult res = CustomMessageBox.Show("Не заполнены поля имя пользователя и пароль", "Ошибка авторизации", MessageBoxButton.OK, eMessageBoxIcons.eWarning);
                _commandStarted = false;
                return;
            }

            AutorizationUserData = new AutorizationUserData(UserName, PasswordString);
            ModelManager.GetInstance().Autontefication(AutorizationUserData);

            WorkingUserData = (ModelManager.GetInstance() as ISystemUser).WorkingUserData;

            if (WorkingUserData == null)
            {
                MessageBoxResult res = CustomMessageBox.Show("Неверное имя пользователя или пароль", "Ошибка авторизации", MessageBoxButton.OK, eMessageBoxIcons.eWarning);
                _commandStarted = false;
                return;
            }
          
            SimpleIoc.Default.GetInstance<BeginPanelPageViewModel>().SetUserData(WorkingUserData);
            Messenger.Default.Send("MainPage");

            _commandStarted = false;
            _strPassword = string.Empty;
        }

        private bool _commandStarted = false;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string PasswordString
        {
            get => _strPassword;
            set
            {
                if (!_commandStarted)
                {
                    _strPassword = value;
                }
            }
        }
        /// <summary>
        /// Строка пароля
        /// </summary>
        private string _strPassword;

        public AutorizationUserData AutorizationUserData { get; private set; }
        public UserData? WorkingUserData { get; private set; }
    }
}
