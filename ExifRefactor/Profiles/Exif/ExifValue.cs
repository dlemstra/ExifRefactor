using System;

namespace ExifRefactor
{
    public abstract class ExifValue : IEquatable<ExifTag>
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

        public static bool operator ==(ExifValue left, ExifTag right) => Equals(left, right);

        public static bool operator !=(ExifValue left, ExifTag right) => !Equals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is ExifTag value)
                return Equals(value);

            return false;
        }

        public bool Equals(ExifTag other) => Tag.Equals(other);

        public override int GetHashCode() => Tag.GetHashCode();
    }
}
