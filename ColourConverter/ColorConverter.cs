using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TaskManager.Pages
{
    public class ColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            // Daily
            if (((DateTime)value).Date == DateTime.Now.Date)
            {
                System.Windows.Media.Color col1 = System.Windows.Media.Color.FromRgb(142, 68, 173);
                SolidColorBrush b2 = new SolidColorBrush(col1);
                return b2;
            }

            // Weekly
            DateTime Firstday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1);
            DateTime Endaday = Firstday.AddDays(6);

            if (((DateTime)value).Date >= Firstday && ((DateTime)value).Date <= Endaday)
            {
                System.Windows.Media.Color col1 = System.Windows.Media.Color.FromRgb(241, 196, 15);
                SolidColorBrush b2 = new SolidColorBrush(col1);
                return b2;
            }

            // Montly
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            DateTime firstDayMonth = new DateTime(year, month, 1);
            DateTime lastDayMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            if (((DateTime)value).Date >= firstDayMonth && ((DateTime)value).Date <= lastDayMonth)
            {
                System.Windows.Media.Color col1 = System.Windows.Media.Color.FromRgb(12, 76, 138);
                SolidColorBrush b2 = new SolidColorBrush(col1);
                return b2;
            }

            // Yearly
            year = DateTime.Now.Year;
            DateTime firstDayYear = new DateTime(year, 1, 1);
            DateTime lastDayYear = new DateTime(year, 12, 31);

            if (((DateTime)value).Date >= firstDayYear && ((DateTime)value).Date <= lastDayYear)
            {
                System.Windows.Media.Color col1 = System.Windows.Media.Color.FromRgb(36, 188, 39);
                SolidColorBrush b2 = new SolidColorBrush(col1);
                return b2;
            }
            else
            {
                System.Windows.Media.Color col1 = System.Windows.Media.Color.FromRgb(33, 44, 55);
                SolidColorBrush b2 = new SolidColorBrush(col1);
                return b2;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush b = value as SolidColorBrush;
            System.Windows.Media.Color col = b.Color;

            return col.R;
        }

    }
}
