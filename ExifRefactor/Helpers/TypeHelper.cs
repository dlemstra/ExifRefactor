using System;

namespace ExifRefactor
{
    internal static class TypeHelper
    {
        public static T[] GetCustomAttributes<T>(Enum value)
          where T : Attribute
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
                return null;

            return (T[])field.GetCustomAttributes(typeof(T), false);
        }
    }
}
