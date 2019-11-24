namespace ExifRefactor
{
    public abstract class ExifValue<TValueType> : IExifValue<TValueType>
    {
        internal ExifValue(ExifTag<TValueType> tag)
        {
            Tag = tag;
        }

        public IExifTag Tag { get; }

        public virtual TValueType Value { get; set; }

        internal abstract ExifDataType DataType { get; }

        internal abstract bool IsArray { get; }

        internal abstract string StringValue { get; }

        bool IExifValue.TrySetValue(object value)
        {
            if (SetValue(value))
            {
                return true;
            }

            return TrySetValue(value);
        }

        ExifDataType IExifValue.DataType => DataType;

        bool IExifValue.IsArray => IsArray;

        public object GetValue() => Value;

        public override string ToString()
        {
            if (Value == null)
                return null;

            var description = ExifTagDescriptionAttribute.GetDescription(Tag, Value);
            if (description != null)
                return description;

            return StringValue;
        }

        protected abstract bool TrySetValue(object value);

        private bool SetValue(object value)
        {
            if (value == null)
            {
                Value = default;
                return true;
            }

            if (value is TValueType typeValue)
            {
                Value = typeValue;
                return true;
            }

            return false;
        }
    }
}
