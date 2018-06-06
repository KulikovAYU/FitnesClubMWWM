using System.Windows;
using System.Windows.Media.Animation;
using FC_EMDB.Classes;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnesClubCL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FitnessClubMWWM.Ui.Desktop.Pages;
using FitnessClubMWWM.Ui.Desktop.Pages.Wind;
using FitnessClubMWWM.Ui.Desktop.Pages.Wind.AbonementInfo;
using GalaSoft.MvvmLight.Ioc;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    public class BeginPanelPageViewModel : ViewModelBase, ISystemUser
    {
        public BeginPanelPageViewModel()
        {
            //ButtonClick = new RelayCommand(ExecuteCommand);
            // _strUserRole = SimpleIoc.Default.GetInstance<AutorizationPageViewModel>().StrUserRole;
        }

        private readonly string _strUserRole;
        private string _strPath;


        #region Команды, отвечающие за показ страниц

        public RelayCommand ShowLogoutPageCommand => new RelayCommand(() => { Messenger.Default.Send("ConfrimExit"); });

        /// <summary>
        /// Команда показать панель администратора (доступна только для админа)
        /// </summary>
        public RelayCommand ShowAdminPanelCommand => new RelayCommand(() => { Messenger.Default.Send("AdminPage"); },
            () =>
            {
                return true; // На момент отладки
                // return WorkingUserData.GetAcsessRigths.AdministrationControls;
                //return false;

            });

        /// <summary>
        /// Команда меню "карты" (группа Карты)
        /// </summary>
        public RelayCommand ShowWorkingCabinetCommand => new RelayCommand(
            () => { Messenger.Default.Send("WorkingCabinetPage"); },
            () =>
            {
                return true;
                // return WorkingUserData.GetAcsessRigths.CardsCreate;
                // return false;.

            });

        /// <summary>
        /// Команда меню показать клиентов (группа клиенты)
        /// </summary>
        public RelayCommand ShowClientPageCommand => new RelayCommand(() => { Messenger.Default.Send("ClientPage"); },
            () =>
            {
                return true; // На момент отладки
                // return WorkingUserData.GetAcsessRigths.ClientsControls;

            });

        /// <summary>
        /// Команда показать расписание трировок (группа расписание занятий)
        /// </summary>
        public RelayCommand ShowClassScheduleCommand =>
            new RelayCommand(() => { Messenger.Default.Send("ClassSchedule"); },
                () =>
                {
                    return true; // На момент отладки
                    //    return WorkingUserData.GetAcsessRigths.SeeTrainingList;

                });

        /// <summary>
        /// Команда показать информацию о персонале (группа Персонал) - доступна всем, кроме редактирования
        /// </summary>
        public RelayCommand ShowStaffPageCommand => new RelayCommand(() => { Messenger.Default.Send("StaffPage"); });


        /// <summary>
        /// Команда тренировка
        /// </summary>
        public RelayCommand TrainingPageCommand => new RelayCommand(() => { new AbonementInfo().Show();
            SimpleIoc.Default.GetInstance<AbonementInfoViewModel>()._Account = null;
        });

        /// <summary>
        /// Команда управления услуг и залами (группа Финансы, услуги, залы)
        /// </summary>
        public RelayCommand ShowSelectActionWindowCommand => new RelayCommand(() =>
        {
            Window selectActionWIndow = new SelectActionWindow();
            selectActionWIndow.ShowDialog();
        }, () =>
        {
            return true; // На момент отладки
            // return WorkingUserData.GetAcsessRigths.FinanceAndServicesControls;

        });

        #region Окно выбора действия

        /// <summary>
        /// Команда закрытия диалогового окна
        /// </summary>
        public RelayCommand<Window> CloseCommand { get; set; } =
            new RelayCommand<Window>((window) => { window?.Close(); });

        private delegate void ShowPages(Window wind, string strPageName);

        /// <summary>
        /// Показать страницу с установкой окладов работникам
        /// </summary>
        public RelayCommand<Window> ShowSalaryPageCommand { get; set; } = new RelayCommand<Window>(
            delegate(Window wind)
            {
                ShowPages del = InvokeShowPages;
                del(wind, "PayPage");

            });


        /// <summary>
        /// Показать страницу с установкой информации о тренировках
        /// </summary>
        public RelayCommand<Window> ShowPriceAbonementPageCommand { get; set; } = new RelayCommand<Window>(
            delegate(Window wind)
            {
                ShowPages del = InvokeShowPages;
                del(wind, "PriceAbonementPage");
            });

        /// <summary>
        /// Показать страницу со списком залов
        /// </summary>
        public RelayCommand<Window> ShowGymsPageCommand { get; set; } = new RelayCommand<Window>(
            delegate(Window wind)
            {
                ShowPages del = InvokeShowPages;
                del(wind, "GymPage");
            });

        //Метод, отвечающий за показ страниц
        static void InvokeShowPages(Window wind, string strPageName)
        {
            wind?.Close();
            Messenger.Default.Send(strPageName);
        }


        #endregion

        #endregion

        public RelayCommand ButtonAgreeWithExitClick => new RelayCommand(AgreeWithExit);

        public RelayCommand ButtonDisagreeWithExitClick => new RelayCommand(DisagreeWithExit);

        void ExecuteCommand()
        {
            MessageBox.Show("Выход из системы");
            Messenger.Default.Send("LoginPage");
        }

        private void AgreeWithExit()
        {
            Messenger.Default.Send("AgreeWithExit");
           


        }

        private void DisagreeWithExit()
        {
            Messenger.Default.Send("DisagreeWithExit");
        }


        /// <summary>
        /// Метод обновляет поля информации о пользователе
        /// </summary>
        public void SetUserData(Employee workingUserData)
        {
            if (workingUserData == null)
            {
                return;
            }

            WorkingUserData = workingUserData;
        }


        /// <summary>
        /// Авторизационные данные пользователя
        /// </summary>
        public AutorizationUserData AutorizationUserData { get; }

        /// <summary>
        /// Рабочие данные пользователя
        /// </summary>
        public Employee WorkingUserData { get; private set; }

        /// <summary>
        /// Команда скрывает боковую панель игнформации
        /// </summary>
        public RelayCommand<Storyboard> HiddenInfoPanelCommand =>
            new RelayCommand<Storyboard>((btn) => { (btn as Storyboard).Begin(); });

      
    }
}
