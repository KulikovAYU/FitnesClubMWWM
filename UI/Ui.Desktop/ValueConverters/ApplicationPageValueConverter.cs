using System;
using System.Diagnostics;
using System.Globalization;
using FitnessClubMWWM.Logic.Ui;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.AdminPanel;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.AdminPanelPages;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.AutorizationPage;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.BeginPanel;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.ConfirmationPages;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.ErrorsPages.ErrAutorization;


namespace FitnessClubMWWM.Ui.Desktop.ValueConverters
{
    /// <summary>
    /// Convert the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>

  public  class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {

        #region PrivateFields
        private AutorizationPage m_autorizationPage;
        private ErrAutorizationPage m_errorautorizationPage;
        private BeginPanelPage m_beginMainPage;
        private TheConfrimLogout m_theConfrimLogout;


        private bool InitializePages()
        {
            if (m_autorizationPage == null)
            {
                m_autorizationPage = new AutorizationPage();
            }

            if (m_errorautorizationPage == null)
            {
                m_errorautorizationPage = new ErrAutorizationPage();
            }

            if (m_beginMainPage == null)
            {
                m_beginMainPage = new BeginPanelPage();
            }

            if (m_theConfrimLogout == null)
            {
                m_theConfrimLogout = new TheConfrimLogout();
            }

            return true;
        }
        #endregion



        public ApplicationPageValueConverter()
        {
         
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Login:
                    return new AutorizationPage();

                case ApplicationPage.AutorizationError:
                    return new ErrAutorizationPage();

                case ApplicationPage.MainPage:
                    return new BeginPanelPage();

                case ApplicationPage.ConfrimExit:
                    return new TheConfrimLogout();

                case ApplicationPage.AdminPanel:
                    return new AdminPanelPage();

                case ApplicationPage.WorkingCabinet:
                    return new WorkingCabinetPage();

                case ApplicationPage.RegisterNewClient:
                   return new RegisterNewClientPage();

                case ApplicationPage.ClientPage:
                    return new ClientPage();

                case ApplicationPage.ClassSchedule:
                    return new SchedulePage();

                default: Debugger.Break();
                        return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


   

}
