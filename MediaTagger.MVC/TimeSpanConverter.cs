using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FubuCore;
using FubuCore.Binding;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MediaTagger.Server
{
    public class TimeSpanConverter : StatelessConverter
    {
        public override bool Matches(PropertyInfo property)
        {
            return property.PropertyType.IsTypeOrNullableOf<TimeSpan>();
        }

        public override object Convert(IPropertyContext context)
        {
            var rawValue = context.RawValueFromRequest.RawValue;

            if (rawValue is TimeSpan) return rawValue;

            var valueString = rawValue.ToString();
            if (String.IsNullOrWhiteSpace(valueString))
                return TimeSpan.Zero;

            var match = Regex.Match(valueString, @"(?:(?<h>\d+)h)?(?:(?<m>\d+)m)?(?:(?<s>\d+)s)?");
            if(!match.Success)
                return TimeSpan.Zero;

            return new TimeSpan(
                ParseGroup(match, "h"),
                ParseGroup(match, "m"),
                ParseGroup(match, "s")
            );
        }

        int ParseGroup(Match match, string groupName)
        {
            var group = match.Groups[groupName];
            if (group == null || String.IsNullOrEmpty(group.Value))
                return 0;

            return int.Parse(group.Value);
        }
    }
}
