using CommonServiceLocator;
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
                SimpleIoc.Default.Register<MainViewModel>(); //регистраци€ view modele-й
                SimpleIoc.Default.Register<BeginPanelPageViewModel>();
                SimpleIoc.Default.Register<AdminPageViewModel>();
                SimpleIoc.Default.Register<AutorizationPageViewModel>();
                SimpleIoc.Default.Register<WorkingCabinetPageViewModel>();
                SimpleIoc.Default.Register<ClientsPageViewModel>();
                SimpleIoc.Default.Register<ScheduleViewModel>();
                SimpleIoc.Default.Register<StaffPaymentViewModel>();
                SimpleIoc.Default.Register<GymPageViewModel>();
                SimpleIoc.Default.Register<PriceAbonementPageViewModel>();
            }
            else
            {
                // Create run time view services and models

                SimpleIoc.Default.Register<MainViewModel>(); //регистраци€ view modele-й
                SimpleIoc.Default.Register<BeginPanelPageViewModel>();
                SimpleIoc.Default.Register<AdminPageViewModel>();
                SimpleIoc.Default.Register<AutorizationPageViewModel>();
                // TODO ¬озможно, придетс€ дополнительно добавить WiewModel дл€ диалогового окна
                SimpleIoc.Default.Register<WorkingCabinetPageViewModel>();
                SimpleIoc.Default.Register<ClientsPageViewModel>();
                SimpleIoc.Default.Register<ScheduleViewModel>();
                SimpleIoc.Default.Register<StaffPaymentViewModel>();
                SimpleIoc.Default.Register<GymPageViewModel>();
                SimpleIoc.Default.Register<PriceAbonementPageViewModel>();
                SimpleIoc.Default.Register<AbonementInfoViewModel>();
            }
       
        }
        /// <summary>
        /// √лавна€ вью модель
        /// </summary>
        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        /// <summary>
        /// ќстальные вью модели (вью модели страниц)
        /// </summary>
        public AutorizationPageViewModel AutorizationViewModel => ServiceLocator.Current.GetInstance<AutorizationPageViewModel>();
        public BeginPanelPageViewModel BeginPanelPageViewModelMain => ServiceLocator.Current.GetInstance<BeginPanelPageViewModel>();
        public AdminPageViewModel AdminPageViewModel => ServiceLocator.Current.GetInstance<AdminPageViewModel>();
        public WorkingCabinetPageViewModel WorkingCabinetPageViewModel => ServiceLocator.Current.GetInstance<WorkingCabinetPageViewModel>();
        public ClientsPageViewModel ClientsPageViewModel => ServiceLocator.Current.GetInstance<ClientsPageViewModel>();
        public ScheduleViewModel SchedulePageViewModel => ServiceLocator.Current.GetInstance<ScheduleViewModel>();
        public StaffPaymentViewModel StaffPaymentPageViewModel => ServiceLocator.Current.GetInstance<StaffPaymentViewModel>();
        public GymPageViewModel GymPageViewModel => ServiceLocator.Current.GetInstance<GymPageViewModel>();
        public PriceAbonementPageViewModel PricePageViewModel => ServiceLocator.Current.GetInstance<PriceAbonementPageViewModel>();

        public AbonementInfoViewModel AbonementInfoViewModel =>
            ServiceLocator.Current.GetInstance<AbonementInfoViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}