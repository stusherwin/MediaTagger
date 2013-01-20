using System;
using System.Text.RegularExpressions;

namespace MediaTagger.Core
{
    public class Duration
    {
        public static readonly Duration Zero = new Duration(TimeSpan.Zero);

        public TimeSpan Value { get; private set; }

        public Duration(TimeSpan value)
        {
            Value = value;
        }

        public Duration CapAt(Duration cap)
        {
            return Value > cap.Value ? cap : this;
        }

        public static Duration FromTimeSpanString(string s)
        {
            return new Duration(TimeSpan.Parse(s));
        }

        public override bool Equals(object obj)
        {
            var other = obj as Duration;

            return other != null
                && other.Value == Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public Duration GetPercentage(double percentage)
        {
            var newTimeSpan = new TimeSpan((long)(Value.Ticks * percentage / 100.0));

            return new Duration(newTimeSpan);
        }

        public static Duration FromHumanReadableString(string valueString)
        {
            if (String.IsNullOrWhiteSpace(valueString))
                return Zero;

            var match = Regex.Match(valueString, @"(?:(?<h>\d+)h)?(?:(?<m>\d+)m)?(?:(?<s>\d+)s)?");
            if (!match.Success)
                return Zero;

            var timeSpan = new TimeSpan(
                ParseGroup(match, "h"),
                ParseGroup(match, "m"),
                ParseGroup(match, "s")
            );

            return new Duration(timeSpan);
        }

        private static int ParseGroup(Match match, string groupName)
        {
            var group = match.Groups[groupName];
            if (group == null || String.IsNullOrEmpty(group.Value))
                return 0;

            return int.Parse(group.Value);
        }
    }
}
