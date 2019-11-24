using System;

namespace ExifRefactor
{
    public abstract class ExifValue : IEquatable<ExifValue>
    {
        internal ExifValue(ExifTag tag)
        {
            Tag = tag;
        }

        public abstract ExifDataType DataType { get; }

        public abstract bool IsArray { get; }

        public ExifTag Tag { get; }

        public abstract object GetValue();

        public abstract bool TrySetValue(object value);

        public static bool operator ==(ExifValue left, ExifValue right) => Equals(left, right);

        public static bool operator !=(ExifValue left, ExifValue right) => !Equals(left, right);

        public static bool operator ==(ExifValue left, ushort right)
        {
            if (left == null)
                return false;

            return left.Tag == right;
        }

        public static bool operator !=(ExifValue left, ushort right)
        {
            if (left == null)
                return true;

            return left.Tag != right;
        }

        public override bool Equals(object obj)
        {
            if (obj is ExifValue value)
                return Equals(value);

            return false;
        }

        public bool Equals(ExifValue other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Tag == other.Tag;
        }

        public override int GetHashCode() => Tag.GetHashCode();
    }
}
