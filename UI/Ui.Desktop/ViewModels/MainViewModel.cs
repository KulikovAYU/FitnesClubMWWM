using System;

using System.Windows;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnesClubCL;
using FitnessClubMWWM.Ui.Desktop.Constants;
using FitnessClubMWWM.Ui.Desktop.DataModels;
using FitnessClubMWWM.Ui.Desktop.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        /// <summary>
        /// Данные пользователя, которые вернула модель
        /// </summary>
        public Employee CurrentEmployee { get; set; }

        #region Private Member


        /// <summary>
        /// The marging around the window to allow drop shadow
        /// </summary>
        //   private int mOutermarginSize = 10;

        /// <summary>
        /// The radius adges of the window
        /// </summary>
        //   private int mWindowRadius = 0;

        #endregion

        #region Public Properties

        public string WindowTitle { get; private set; }
     
        /// <summary>
        /// The size of tre resize border around the window
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// The size of tre resize border around the window, taking into account the outer marging
        /// </summary>
        // public Thickness ResizeBorderThickness => new Thickness(ResizeBorder /*+ OutermarginSize*/);

        /// <summary>
        /// The marging around the window to allow drop shadow
        /// </summary>
        //public int OutermarginSize
        //{
        //    get => mWindowState == WindowState.Maximized ? 0 : mOutermarginSize;
        //    set => mOutermarginSize = value;
        //}

        ///// <summary>
        ///// The marging around the window to allow drop shadow
        ///// </summary>
        //public Thickness OutermarginSizeThickness => new Thickness(OutermarginSize);

        ///// <summary>
        ///// The radius adges of the window
        ///// </summary>
        //public int WindowRadius
        //{
        //    get => mWindowState == WindowState.Maximized ? 0 : mWindowRadius;
        //    set => mWindowRadius = value;
        //}

        ///// <summary>
        ///// Get the state of the window
        ///// </summary>
        //public WindowState mWindowState { get; set; }

        ///// <summary>
        ///// The radius edges of the window
        ///// </summary>
        //public CornerRadius WindowCornerRadius =>new CornerRadius(WindowRadius);


        //public EventHandler OnStateChanged { get; set; }

        ///// <summary>
        ///// The height of the title bar / caption of the window
        ///// </summary>
        public int TitleHeight { get; set; } = 42;

        ///// <summary>
        ///// The height of the title bar / caption of the window
        ///// </summary>
        //public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);


        // <summary>
        // Текущая страница приложения
        // </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.MainPage; // ApplicationPage.Login;  ApplicationPage.MainPage


        #region Команды управления состояния окна

        public RelayCommand CloseCommand { get; set; } = new RelayCommand(() =>
        {
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
        });



        public RelayCommand MinMaxCommand { get; set; } = new RelayCommand(() =>
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.WindowState ^= WindowState.Maximized;
        });

        public RelayCommand MinimizeCommand { get; set; } = new RelayCommand(() =>
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
        });


        #endregion

        /// <summary>
        /// Закрыть дочерние диалоговые окна
        /// </summary>
        public RelayCommand<Window> CloseChildWindowsCommand => new RelayCommand<Window>((window)=> window?.Close());

        #endregion

        void ProcessMessage(string msg)
        {
          
            switch (msg)
            {
                case "LoginPage":
                case "AgreeWithExit":
                    CurrentPage = ApplicationPage.Login;
                    AboutUserPanelVisibility = Visibility.Hidden;
                    break;
                case "AutorizationError":
                    CurrentPage = ApplicationPage.AutorizationError;
                    break;
                case "ConfrimExit":
                    CurrentPage = ApplicationPage.ConfrimExit;
                    break;
                case "DisagreeWithExit":
                    CurrentPage = ApplicationPage.MainPage;
                    break;
                case "AdminPage":
                    
                    CurrentPage = ApplicationPage.AdminPanel;
                    break;

                case "MainPage":
                    CurrentPage = ApplicationPage.MainPage;
                    break;

                case "WorkingCabinetPage":
                    CurrentPage = ApplicationPage.WorkingCabinet;
                    break;

                case "RegisterNewClientPage":
                    CurrentPage = ApplicationPage.RegisterNewClient;
                    break;

                case "ClientPage":
                    CurrentPage = ApplicationPage.ClientPage;
                    break;

                case "ClassSchedule":
                    CurrentPage = ApplicationPage.ClassSchedule;
                    break;

                case "StaffPage":
                    CurrentPage = ApplicationPage.StaffPage;
                    break;

                case "PayPage":
                    CurrentPage = ApplicationPage.PayPage;
                    break;

                case "PriceAbonementPage":
                    CurrentPage = ApplicationPage.PriceAbonementPage;
                    break;

                case "GymPage":
                    CurrentPage = ApplicationPage.GymPage;
                    break;

            }
        }




      

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                WindowTitle = "Информационная система фитнес-клуб (Design)";
                // Code runs in Blend --> create design time data.
            }
            else
            {
                WindowTitle = "Информационная система фитнес-клуб";
                // Code runs "for real"
            }

            Messenger.Default.Register(this, new Action<string>(ProcessMessage));

            Messenger.Default.Register(this, new Action<Exception>(ErrorMessage));

            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.MouseLeftButtonDown += (sender, e) =>
                {
                    Application.Current.MainWindow.DragMove();
                };
            }


            ModelManager.GetInstance.CreateDB();
        }

        private void ErrorMessage(Exception obj)
        {
            CustomMessageBox.Show(obj.Message, "Исключение", MessageBoxButton.OK, eMessageBoxIcons.eWarning);
        }

        #endregion

        /// <summary>
        /// Видимость панели о пользователе
        /// </summary>
        public Visibility AboutUserPanelVisibility
        {
            get => CurrentPage != ApplicationPage.Login ? Visibility.Visible : Visibility.Hidden;
            set => AboutUserPanelVisibility = value;
        }


    }
}