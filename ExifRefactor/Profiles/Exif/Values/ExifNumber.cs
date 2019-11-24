using System;
using System.Globalization;

namespace ExifRefactor
{
    internal sealed class ExifNumber : ExifValue<Number>
    {
        internal ExifNumber(ExifTag<Number> tag)
            : base(tag)
        {
        }

        public override ExifDataType DataType
        {
            get
            {
                if (Value > ushort.MaxValue)
                    return ExifDataType.Long;

                return ExifDataType.Short;
            }
        }

        public override bool IsArray => false;

        internal override string StringValue => ((uint)Value).ToString(CultureInfo.InvariantCulture);

        protected override bool TrySetValueFromObject(object value)
        {
            switch (value)
            {
                case int intValue:
                    Value = intValue;
                    return true;
                case uint uintValue:
                    Value = uintValue;
                    return true;
                case short shortValue:
                    Value = shortValue;
                    return true;
                case ushort ushortValue:
                    Value = ushortValue;
                    return true;
                default:
                    return false;
            }
        }
    }
}
