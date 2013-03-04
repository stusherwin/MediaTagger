using System;

namespace MediaTagger.Core
{
    public class Percentage
    {
        private readonly double _ratio;

        public Percentage(double percentage)
        {
            if(percentage < 0 || percentage > 100)
                throw new ArgumentException("Percentage must be in the range 0 to 100");

            _ratio = percentage / 100.0;
        }

        public long PercentageOf(long fullValue)
        {
            return (long)(fullValue * _ratio);
        }
    }
}