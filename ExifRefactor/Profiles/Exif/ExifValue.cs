using System;

namespace ExifRefactor
{
    public abstract class ExifValue : IEquatable<ushort>
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

        public static bool operator ==(ExifValue left, ushort right) => Equals(left, right);

        public static bool operator !=(ExifValue left, ushort right) => !Equals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is ushort value)
                return Equals(value);

            return false;
        }

        public bool Equals(ushort other) => Tag == other;

        public override int GetHashCode() => Tag.GetHashCode();
    }
}
