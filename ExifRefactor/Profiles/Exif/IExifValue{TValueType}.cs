namespace ExifRefactor
{
    public interface IExifValue<TValueType> : IExifValue
    {
        TValueType Value { get; set; }
    }
}
