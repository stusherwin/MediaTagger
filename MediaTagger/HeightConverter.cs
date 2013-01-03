using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MediaTagger
{
    public class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double baseWidth = double.Parse(((string)parameter).Split(',')[0]);
            double baseHeight = double.Parse(((string)parameter).Split(',')[1]);
            double margin = double.Parse(((string)parameter).Split(',')[2]);

            double containerWidth = (double)value;

            double newWidth = WidthCalculator.CalculateWidth(containerWidth, baseWidth, margin);
            double newHeight = newWidth * (baseHeight / baseWidth);

            return newHeight;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
