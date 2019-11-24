using System;
using System.Globalization;

namespace ExifRefactor
{
    internal static class EnumHelper
    {
        public static bool HasFlag<TEnum>(TEnum value, TEnum flag)
          where TEnum : struct, IConvertible
        {
            uint flagValue = flag.ToUInt32(CultureInfo.InvariantCulture);
            return (value.ToUInt32(CultureInfo.InvariantCulture) & flagValue) == flagValue;
        }

        public static TEnum Parse<TEnum>(ushort value, TEnum defaultValue)
          where TEnum : struct, IConvertible
        {
            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if (value == enumValue.ToUInt16(CultureInfo.InvariantCulture))
                    return enumValue;
            }

            return defaultValue;
        }
    }
}
