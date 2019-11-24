using System.Globalization;

namespace ExifRefactor
{
    internal sealed class ExifLong : ExifValue<uint>
    {
        internal ExifLong(ExifTag<uint> tag)
            : base(tag)
        {
        }

        public override ExifDataType DataType => ExifDataType.Long;

        public override bool IsArray => false;

        internal override string StringValue => Value.ToString(CultureInfo.InvariantCulture);

        protected override bool TrySetValueFromObject(object value)
        {
            switch (value)
            {
                case int intValue:
                    Value = (uint)intValue;
                    return true;
                default:
                    return false;
            }
        }
    }
}
