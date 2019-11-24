namespace ExifRefactor
{
    public abstract class ExifValue<TValueType> : ExifValue
    {
        internal ExifValue(ExifTag<TValueType> tag)
            : base(tag)
        {
        }

        public virtual TValueType Value { get; set; }

        internal abstract string StringValue { get; }

        public override object GetValue() => Value;

        public override string ToString()
        {
            if (Value == null)
                return null;

            var description = ExifTagDescriptionAttribute.GetDescription(Tag, Value);
            if (description != null)
                return description;

            return StringValue;
        }

        public override bool TrySetValue(object value)
        {
            if (SetValue(value))
            {
                return true;
            }

            return TrySetValueFromObject(value);
        }

        protected abstract bool TrySetValueFromObject(object value);

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
