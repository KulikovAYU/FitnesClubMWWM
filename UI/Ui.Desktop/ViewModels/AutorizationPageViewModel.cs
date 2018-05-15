using System.Windows;
using FitnesClubCL;
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
   public class AutorizationPageViewModel : ViewModelBase
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

            ModelManager.GetInstance().Autontefication(UserName, PasswordString, out var strRole);
          
            if (UserName == null && PasswordString == null && strRole == null)
            {
                MessageBoxResult res = CustomMessageBox.Show("Не заполнены поля имя пользователя и пароль", "Ошибка авторизации", MessageBoxButton.OK, eMessageBoxIcons.eWarning);
            }

            if ((UserName != null || PasswordString != null) && strRole == null)
            {
                MessageBoxResult res = CustomMessageBox.Show("Неверное имя пользователя или пароль", "Ошибка авторизации", MessageBoxButton.OK, eMessageBoxIcons.eWarning);
            }

            StrUserRole = strRole;

            if (strRole != null)
            {
                Messenger.Default.Send("MainPage");
            }

            _commandStarted = false;
            _strPassword = "";
            SimpleIoc.Default.GetInstance<BeginPanelPageViewModel>().UserFullName = (ModelManager.GetInstance() as ISystemUser).UserFullName;
        }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string StrUserRole { get; private set; } = null;


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
      


    }
}
