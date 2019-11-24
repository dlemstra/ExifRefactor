namespace ExifRefactor
{
    public abstract class ExifValue
    {
        public ExifValue(IExifTag tag)
        {
            Tag = tag;
        }

        public abstract ExifDataType DataType { get; }

        public abstract bool IsArray { get; }

        public IExifTag Tag { get; }

        public abstract object GetValue();

        public abstract bool TrySetValue(object value);
    }
}
