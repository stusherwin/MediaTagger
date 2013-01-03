using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaTagger
{
    public static class WidthCalculator
    {
        public static double CalculateWidth(double containerWidth, double baseWidth, double margin)
        {
            double baseWidthsPerWidth = Math.Floor(containerWidth / (baseWidth + (margin * 2)));

            if (baseWidthsPerWidth == 0)
                return baseWidth;

            var newWidth = (containerWidth) / baseWidthsPerWidth - (margin * 2);

            return newWidth;
        }
    }
}
