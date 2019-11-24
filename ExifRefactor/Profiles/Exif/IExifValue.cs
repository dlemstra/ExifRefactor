namespace ExifRefactor
{
    public interface IExifValue
    {
        ExifDataType DataType { get; }

        bool IsArray { get; }

        IExifTag Tag { get; }

        object GetValue();

        bool TrySetValue(object value);
    }
}
