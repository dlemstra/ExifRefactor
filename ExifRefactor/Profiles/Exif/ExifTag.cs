using System;

namespace ExifRefactor
{
    public abstract class ExifTag : IEquatable<ExifTag>
    {
        private readonly ushort _value;

        internal ExifTag(ushort value)
        {
            _value = value;
        }

        public static bool operator ==(ExifTag left, ExifTag right) => Equals(left, right);

        public static bool operator !=(ExifTag left, ExifTag right) => !Equals(left, right);

        public static explicit operator ushort(ExifTag tag) => tag._value;

        public override bool Equals(object obj)
        {
            if (obj is ExifValue value)
                return Equals(value);

            return false;
        }

        public bool Equals(ExifTag other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return _value == other._value;
        }

        public override int GetHashCode() => _value.GetHashCode();

        public static ExifTag<uint> SubIFDOffset { get; } = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

        public static ExifTag<uint> GPSIFDOffset { get; } = new ExifTag<uint>(ExifTagValue.GPSIFDOffset);

        public static ExifTag<uint> CodingMethods { get; } = new ExifTag<uint>(ExifTagValue.CodingMethods);

        public static ExifTag<uint> JPEGInterchangeFormat { get; } = new ExifTag<uint>(ExifTagValue.JPEGInterchangeFormat);

        public static ExifTag<uint> JPEGInterchangeFormatLength { get; } = new ExifTag<uint>(ExifTagValue.JPEGInterchangeFormatLength);

        public static ExifTag<Number> PixelXDimension { get; } = new ExifTag<Number>(ExifTagValue.PixelXDimension);

        internal static ExifTag Get(ExifTagValue tag) => (tag) switch
        {
            ExifTagValue.SubIFDOffset => SubIFDOffset,
            ExifTagValue.GPSIFDOffset => GPSIFDOffset,
            ExifTagValue.CodingMethods => CodingMethods,
            ExifTagValue.JPEGInterchangeFormat => JPEGInterchangeFormat,
            ExifTagValue.JPEGInterchangeFormatLength => JPEGInterchangeFormatLength,
            ExifTagValue.PixelXDimension => PixelXDimension,

            _ => null
        };
    }
}
