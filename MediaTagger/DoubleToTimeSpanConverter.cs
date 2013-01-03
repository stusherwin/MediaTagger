using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace MediaTagger
{
    public class DoubleToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value, targetType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value, targetType);
        }

        private object Convert(object value, Type targetType)
        {
            if (value is double && targetType.Equals(typeof(TimeSpan)))
            {
                return TimeSpan.FromMilliseconds((double)value);
            }
            else if (value is TimeSpan && targetType.Equals(typeof(double)))
            {
                return ((TimeSpan)value).TotalMilliseconds;
            }
            return null;
        }
    }

    public class DoubleToDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value, targetType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value, targetType);
        }

        private object Convert(object value, Type targetType)
        {
            if (value is double && targetType.Equals(typeof(Duration)))
            {
                return new Duration(TimeSpan.FromMilliseconds((double)value));
            }
            else if (value is Duration && targetType.Equals(typeof(double)))
            {
                var duration = (Duration)value;
                if(duration.HasTimeSpan)
                    return duration.TimeSpan.TotalMilliseconds;

                return 1000 * 60 * 30;
            }
            return null;
        }
    }
}
