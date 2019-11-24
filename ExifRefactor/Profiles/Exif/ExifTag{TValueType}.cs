using System.Diagnostics;

namespace ExifRefactor
{
    [DebuggerDisplay("{(ExifTagValue)(ushort)this}")]
    public sealed class ExifTag<TValueType> : ExifTag
    {
        internal ExifTag(ExifTagValue value)
            : base((ushort)value)
        {
        }
    }
}
