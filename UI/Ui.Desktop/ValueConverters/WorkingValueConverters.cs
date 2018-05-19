using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FitnesClubCL.Classes;
using FitnessClubMWWM.Logic.Ui;
using FitnessClubMWWM.Ui.Desktop.DataModels;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages;

// Вспомогательные конвертеры значений
namespace FitnessClubMWWM.Ui.Desktop.ValueConverters
{
    public class AdminPageValueConverter : BaseValueConverter<AdminPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ApplicationPage page)) return null;

            switch (page)
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

    /// <summary>
    /// Конвертер для DataGridRowDetails (служит для скрытия или показа RowDetails)
    /// </summary>
    public class VisibilityToNullableBooleanConverter : BaseValueConverter<VisibilityToNullableBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility visibility ? visibility == Visibility.Visible : Binding.DoNothing;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is bool b)
                return (b ? Visibility.Visible : Visibility.Collapsed);
            return Binding.DoNothing;
        }
    }



    public class IsCheckedToogleConverter : BaseValueConverter<IsCheckedToogleConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value != null && !(bool)value;

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value != null && ((bool)value);
    }

    /// <summary>
    /// Класс представляет конвертер даты в формад дд:мм:гг
    /// </summary>
    public class DateTimeValueConverter : BaseValueConverter<DateTimeValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime)) return null;
            return ((DateTime)value).Date.Day + "." + ((DateTime) value).Date.Month + "." + ((DateTime) value).Date.Year;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Конвертер для фотографии пользователя
    /// </summary>
    public class ImageBrushValueConverter : BaseValueConverter<ImageBrushValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                return "/FitnessClubMWWM.Ui.Desktop;component/Images/Icons/red_gym_icon.ico";
            return (value as string).Length == 0
                ? "/FitnessClubMWWM.Ui.Desktop;component/Images/Icons/red_gym_icon.ico"
                : value;

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Конвертер для команды "сохранить изменения"
    /// </summary>
    public class MultiConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Tuple<string, string, string> tuple = new Tuple<string, string, string>((string)values[0], (string)values[1], (string)values[2]);
            return tuple;
            //NewClientData data = new NewClientData();
            //data.ClientName = values[0] as string;
            //data.ClientFamily = values[1] as string;
            //return data;
            //return values.ToArray(); //второй вариант
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
