using System.Windows;
using FC_EMDB.Classes;
using FC_EMDB.EMDB.CF.Data.Domain;
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

            if (string.IsNullOrEmpty(AutorizationUserData.UserLoginName) && string.IsNullOrEmpty(AutorizationUserData.PasswordString))
            {
                MessageBoxResult res = CustomMessageBox.Show("Не заполнены поля имя пользователя и пароль", "Ошибка авторизации", MessageBoxButton.OK, eMessageBoxIcons.eWarning);
                _commandStarted = false;
                return;
            }

            //Аутентификация
            ModelManager.GetInstance.Autontefication(AutorizationUserData);
           
            if (ModelManager.GetInstance.WorkingUserData != null)
            {
                WorkingUserData = ModelManager.GetInstance.WorkingUserData;//данные пользователя в случае успеха
                SimpleIoc.Default.GetInstance<BeginPanelPageViewModel>().SetUserData(WorkingUserData);
                Messenger.Default.Send("MainPage");
            }
            else
            {
                MessageBoxResult res = CustomMessageBox.Show("Неверное имя пользователя или пароль", "Ошибка авторизации", MessageBoxButton.OK, eMessageBoxIcons.eWarning);
                _commandStarted = false;
                return;
            }

            _commandStarted = false;
        }

        private bool _commandStarted = false;
        /// <summary>
        /// Имя пользователя
        /// </summary>
      

        /// <summary>
        /// Авторизационные данные пользователя
        /// </summary>
        public AutorizationUserData AutorizationUserData { get;  private set; } = new AutorizationUserData();


        /// <summary>
        /// Данные пользователя, которые вернула модель
        /// </summary>
        public Employee WorkingUserData { get; private set; }
    }
}
