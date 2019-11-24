using System;

namespace ExifRefactor
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal sealed class ExifTagDescriptionAttribute : Attribute
    {
        private readonly object _value;
        private readonly string _description;

        public ExifTagDescriptionAttribute(object value, string description)
        {
            _value = value;
            _description = description;
        }

        public static string GetDescription(ExifTag tag, object value)
        {
            var attributes = TypeHelper.GetCustomAttributes<ExifTagDescriptionAttribute>((ExifTagValue)(ushort)tag);

            if (attributes == null || attributes.Length == 0)
                return null;

            foreach (var attribute in attributes)
            {
                if (Equals(attribute._value, value))
                    return attribute._description;
            }

            return null;
        }
    }
}
