using System.Diagnostics;

namespace ExifRefactor
{
    [DebuggerDisplay("{(ExifTagValue)Value}")]
    public struct ExifTag<TValueType> : IExifTag
    {
        internal ExifTag(ExifTagValue value) => Value = (ushort)value;

        public ushort Value { get; }

        public override bool Equals(object obj)
        {
            if (obj is IExifTag tag)
            {
                return Equals(tag);
            }

            return false;
        }

        public static implicit operator uint(ExifTag<TValueType> tag) => tag.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
