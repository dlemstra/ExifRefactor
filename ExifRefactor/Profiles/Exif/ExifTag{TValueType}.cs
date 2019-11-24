namespace ExifRefactor
{
    public sealed class ExifTag<TValueType> : ExifTag
    {
        internal ExifTag(ExifTagValue value)
            : base((ushort)value)
        {
        }
    }
}
