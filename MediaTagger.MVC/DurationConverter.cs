using System;
using FubuCore;
using FubuCore.Binding;
using System.Reflection;
using MediaTagger.Core;

namespace MediaTagger.Mvc
{
    public class DurationConverter : StatelessConverter
    {
        public override bool Matches(PropertyInfo property)
        {
            return property.PropertyType.IsTypeOrNullableOf<Duration>();
        }

        public override object Convert(IPropertyContext context)
        {
            var rawValue = context.RawValueFromRequest.RawValue;

            if (rawValue is Duration)
                return rawValue;

            if (rawValue is TimeSpan) 
                return new Duration((TimeSpan)rawValue);

            return Duration.FromHumanReadableString(rawValue.ToString());
        }
    }
}
