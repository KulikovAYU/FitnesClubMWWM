using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnessClubMWWM.Logic.Ui;
using FitnessClubMWWM.Ui.Desktop.DataModels;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages;
using FitnessClubMWWM.Ui.Desktop.Pages.SlidePages.AbonementInfoPages;


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
    /// Конвертер для страниц информации об абонементе
    /// </summary>
    public class AbonementInfoValueConverter : BaseValueConverter<AbonementInfoValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if (!(value is ApplicationPage page)) return null;

            switch (page)
            {
                case ApplicationPage.AbonementInfoDetails:
                    return new AbonementInformationPage();
                case ApplicationPage.TrainingAndServiceList:
                    return new TrainingAndServiceList();
                case ApplicationPage.LongAbonement:
                    return new LongAbonementPage();
                case ApplicationPage.PayServicesAndTrainings:
                    return new PayTrainingOrServicePage();

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
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value != null && !(bool) value;

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            value != null && ((bool) value);
    }

    /// <summary>
    /// Класс представляет конвертер даты в формад дд:мм:гг
    /// </summary>
    public class DateTimeValueConverter : BaseValueConverter<DateTimeValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime)) return null;
            return ((DateTime) value).Date.Day + "." + ((DateTime) value).Date.Month + "." +
                   ((DateTime) value).Date.Year;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Конвертер для фотографий
    ///  </summary>
    public class ImageBrushValueConverter : BaseValueConverter<ImageBrushValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new BitmapImage(new Uri("pack://application:,,,/Images/Icons/red_gym_icon.ico", UriKind.RelativeOrAbsolute));//TODO: раскомментить
                //return null;

            }

            if (value is byte[] _val)
            {
                MemoryStream ms = new MemoryStream(_val);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                bi.Freeze();
                return bi;
            }

            return null;
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

        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            //  Tuple<string, DateTime> newtuple = new Tuple<string, DateTime>((string)values[7], (DateTime)values[8]);

            if (values[8] == null || values[10] == null)
                return null;
            Tuple<string, string, string, bool, string, string, string, Tuple<string, DateTime, string, DateTime>> tuple
                =
                new Tuple<string, string, string, bool, string, string, string,
                    Tuple<string, DateTime, string, DateTime>>((string) values[0],
                    (string) values[1], (string) values[2], (bool) values[3], (string) values[4], (string) values[5],
                    (string) values[6],
                    new Tuple<string, DateTime, string, DateTime>((string) values[7], (DateTime) values[8],
                        (string) values[9], (DateTime) values[10]));

            return tuple;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Конвертер для пересчета суммы тренировки
    /// </summary>
    public class MultiPriceValueConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return null;


            List<object> arrList = values.ToList();

            PriceTrainingList priceTrainingList = null;
            string _strCountTrain;
         
            int _nTrainingCount = 0;
            Decimal? _totalCost = 0.0m;


            foreach (var elems in arrList)
            {
                if (elems is PriceTrainingList)
                {
                    priceTrainingList = elems as PriceTrainingList;
                }

              
                if (elems is string)
                {
                    _strCountTrain = elems as string;
                    if (string.IsNullOrEmpty(_strCountTrain))
                        _strCountTrain = new string('0', 0);
                    else
                    {
                        _nTrainingCount = Int32.Parse(_strCountTrain);
                    }

                }

                if (elems is Decimal)
                {
                    _totalCost = elems as Decimal?;
                }
            }

            _totalCost = priceTrainingList?.TrainingCurrentCost * _nTrainingCount;

            if (_totalCost == null)
            {
                return string.Empty;
            }

            return Decimal.Round(Decimal.Parse(_totalCost.ToString()), 2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class TotalCostValueConverter : BaseValueConverter<TotalCostValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (string.IsNullOrEmpty(value as string))
                return null;

            return Decimal.Parse(value as string);

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Конвертер для пересчета суммы тренировки
    /// </summary>
    //public class MultiSellValueConverter : IMultiValueConverter
    //{
    //    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return values.Contains(null) ? null : new Tuple<Decimal>((Decimal) values[0]);
    //    }

    //    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    public class ServicesInSubscriptionValueConverter : BaseValueConverter<ServicesInSubscription>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection<ServicesInSubscription>)
            {
                return new ObservableCollection<ServicesInSubscription>((value as ICollection<ServicesInSubscription>)
                    .Cast<ServicesInSubscription>());
            }

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeToDateConverter : BaseValueConverter<DateTimeToDateConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (parameter != null && parameter.ToString() == "EN")
                return ((DateTime) value).ToString("MM-dd-yyyy");

            return ((DateTime) value).ToString("dd.MM.yyyy");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    /// <summary>
    /// Конвертер для опции активировать абонемент
    /// </summary>
    public class ActivateAccountConverter : BaseValueConverter<ActivateAccountConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Account account)
            {
               Abonement abonem =  account.Abonement;
                if (parameter as string == "Freeze")
                {
                    return (abonem as Abonement).AbonmentStatus.StatusName == "Активен";
                }

                return (abonem as Abonement).AbonmentStatus.StatusName != "Активен";

            }

            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Конвертер для имени тренера
    /// </summary>
    public class GetTrenerNameConverter : BaseValueConverter<GetTrenerNameConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UpcomingTraining upcomingTraining)
            {
               return upcomingTraining.Employee.HumanFullName;
            }

            return string.Empty;


        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Конвертер для имени тренера
    /// </summary>
    public class GetTrainingStatusConverter : BaseValueConverter<GetTrainingStatusConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UpcomingTraining upcomingTraining)
            {
                return upcomingTraining.StatusTraining.StatusName;
            }

            return string.Empty;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsAvailableCreateRecordOntraining : BaseValueConverter<IsAvailableCreateRecordOntraining>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UpcomingTraining upcomingTraining)
            {
                return upcomingTraining.StatusTraining.StatusName == "Группа заполнена, запись невозможна";
            }

            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }




    /// <summary>
    /// Класс представляет конвертер даты в формад дд:мм:гг+ часы и минуты
    /// </summary>
    public class NumberAndTimeUpcomingTrainingValueConverter : BaseValueConverter<NumberAndTimeUpcomingTrainingValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime)) return null;
            var Day = ((DateTime) value).Date.Day; 
            var Mounth = ((DateTime) value).Date.Month;
            var Year = ((DateTime)value).Date.Year;
            var Hours = ((DateTime) value).TimeOfDay.Hours;
            var Minutes = ((DateTime) value).TimeOfDay.Minutes;

            return $"{Day}.{Mounth}.{Year}  {Hours} ч.  {Minutes} мин." ;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Класс представляет конвертер для подсчета общего количества тренировок
    /// </summary>
    public class CountTrainingValueConverter : BaseValueConverter<CountTrainingValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int nTrainingCount = 0;
            if (value is Abonement abonement)
            {
                foreach (var service in abonement.ArrServicesInSubscription)
                {
                    nTrainingCount += service.SiSTrainingCount;
                }
            }

            return nTrainingCount;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    /// <summary>
    /// Класс представляет конвертер для отображения общей стоимости абонемента
    /// </summary>
    public class TotalCostInfoValueConverter : BaseValueConverter<TotalCostInfoValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int nTrainingCount = 0;
            if (value is Abonement abonement)
            {
                foreach (var service in abonement.ArrServicesInSubscription)
                {
                    //service.TotalCost
                    //nTrainingCount += service.SiSTrainingCount;
                }
            }

            return nTrainingCount;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
