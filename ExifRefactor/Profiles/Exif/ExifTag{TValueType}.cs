using System.Diagnostics;

namespace ExifRefactor
{
    [DebuggerDisplay("{(ExifTagValue)Value}")]
    public struct ExifTag<TValueType> : IExifTag
    {
        internal ExifTag(ExifTagValue value) => Value = (ushort)value;

        public ushort Value { get; }

        public static bool operator ==(ExifTag<TValueType> left, ExifTag<TValueType> right) => Equals(left, right);

        public static bool operator !=(ExifTag<TValueType> left, ExifTag<TValueType> right) => !Equals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is IExifTag tag)
            {
                return Equals(tag);
            }

            return false;
        }

        public bool Equals(IExifTag other) => Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
