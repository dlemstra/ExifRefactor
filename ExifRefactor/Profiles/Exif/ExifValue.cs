using System;

namespace ExifRefactor
{
    public abstract class ExifValue : IEquatable<ExifValue>
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

        public static bool operator ==(ExifValue left, ExifValue right) => Equals(left, right);

        public static bool operator !=(ExifValue left, ExifValue right) => !Equals(left, right);

        public static bool operator ==(ExifValue left, uint right) => left.Tag.Value == right;

        public static bool operator !=(ExifValue left, uint right) => left.Tag.Value != right;

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

            return Tag.Value == other.Tag.Value;
        }

        public override int GetHashCode() => Tag.GetHashCode();
    }
}
