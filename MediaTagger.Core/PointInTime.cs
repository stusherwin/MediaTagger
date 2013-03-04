namespace MediaTagger.Core
{
    public class PointInTime
    {
        private Duration _absoluteDuration;
        private Percentage _percentageOfDuration;

        public PointInTime(Duration absoluteDuration)
        {
            _absoluteDuration = absoluteDuration;
        }

        public PointInTime(Percentage percentageOfDuration)
        {
            _percentageOfDuration = percentageOfDuration;
        }

        public Duration ResolveAgainst(Duration fullDuration)
        {
            if (_absoluteDuration != null)
                return _absoluteDuration.CapAt(fullDuration);

            return fullDuration.GetPercentage(_percentageOfDuration);
        }
    }
}