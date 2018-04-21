using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using FitnessClubMWWM.Logic.Ui;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.AdminPanelPages;

namespace FitnessClubMWWM.Ui.Desktop.ValueConverters
{
    class WorkingValueConverters
    {
    }

    public class AdminPageValueConverter : BaseValueConverter<AdminPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.AddUserPrivilegyPage:
                    return new AddUserPrivilegyPage();
                case ApplicationPage.AddNewUserPage:
                    return new AddNewUserPage();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



  
}
