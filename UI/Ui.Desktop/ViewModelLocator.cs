using CommonServiceLocator;
using FitnessClubMWWM.Logic.Ui;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.AutorizationPage;
using FitnessClubMWWM.Ui.Desktop.ViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace FitnessClubMWWM.Ui.Desktop
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
            }
            else
            {
                // Create run time view services and models
            }

            SimpleIoc.Default.Register<MainViewModel>(); //регистрация view modele-й
            SimpleIoc.Default.Register<BeginPanelPageViewModel>();
            SimpleIoc.Default.Register<AdminPageViewModel>();
            SimpleIoc.Default.Register<AutorizationPageViewModel>();
            SimpleIoc.Default.Register<MessageWindowViewModel>();
        }


        public AutorizationPageViewModel AutorizationViewModel => ServiceLocator.Current.GetInstance<AutorizationPageViewModel>();
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public BeginPanelPageViewModel BeginPanelPageViewModelMain => ServiceLocator.Current.GetInstance<BeginPanelPageViewModel>();
        public AdminPageViewModel AdminPageViewModel => ServiceLocator.Current.GetInstance<AdminPageViewModel>();
        public MessageWindowViewModel MessageWindowViewModel => ServiceLocator.Current.GetInstance<MessageWindowViewModel>();
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}