using System;
using System.Text.RegularExpressions;
using MediaTagger.Core.Thumbnails;

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

        public Duration GetPercentage(Percentage percentage)
        {
            return new Duration(new TimeSpan(percentage.PercentageOf(Value.Ticks)));
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

        public override string ToString()
        {
            return Value.ToString();
        }

        public string ToTicksString()
        {
            return Value.Ticks.ToString();
        }

        public static Duration FromTimeSpanString(string s)
        {
            return new Duration(TimeSpan.Parse(s));
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
