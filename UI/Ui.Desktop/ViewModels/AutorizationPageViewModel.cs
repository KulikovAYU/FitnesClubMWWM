using System.Windows;
using FC_EMDB.Constants;
using FitnesClubCL;
using FitnessClubMWWM.Ui.Desktop.Constants;
using FitnessClubMWWM.Ui.Desktop.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    /// <summary>
    /// Данная вью модель управляет авторизацией пользователя
    /// </summary>
   public class AutorizationPageViewModel : ViewModelBase
    {
        public RelayCommand Autentification => new RelayCommand(()=>
        {
            _commandStarted = true;// TODO: пока сделал костыль. Если будет время, то подумать
            eRoles role = eRoles.eNone;
            ModelManager.GetInstance().Autontefication(UserName, PasswordHash, out role);
            _commandStarted = false;
            if (role == eRoles.eNone)
            {
                MessageBoxResult res = CustomMessageBox.Show("Неверное имя пользователя или пароль", "Ошибка авторизации", MessageBoxButton.OK, eMessageBoxIcons.eWarning);
                if (res == MessageBoxResult.OK)
                {
                    return;
                }
                
            }
            Messenger.Default.Send("MainPage");
        });

        private bool _commandStarted = false;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string PasswordHash
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

        private string _strPassword;

    }
}
