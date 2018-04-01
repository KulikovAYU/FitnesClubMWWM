using System;
using System.Diagnostics;
using System.Globalization;
using FitnessClubMWWM.Logic.Ui;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.AutorizationPage;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.BeginPanel;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.ErrorsPages.ErrAutorization;

namespace FitnessClubMWWM.Ui.Desktop.ValueConverters
{
    /// <summary>
    /// Convert the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>

  public  class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
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
