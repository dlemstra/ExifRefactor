using System;

namespace ExifRefactor
{
    public static partial class ExifValues
    {
        internal static ExifValue Create(ExifTag tag) => (ExifValue)CreateValue(tag);

        internal static ExifValue<TValueType> Create<TValueType>(ExifTag<TValueType> tag) => (ExifValue<TValueType>)CreateValue(tag);

        private static object CreateValue(ExifTag tag) => ((ExifTagValue)(uint)tag) switch
        {
            ExifTagValue.SubIFDOffset => new ExifLong(ExifTag.SubIFDOffset),
            ExifTagValue.JPEGInterchangeFormat => new ExifLong(ExifTag.JPEGInterchangeFormat),
            ExifTagValue.JPEGInterchangeFormatLength => new ExifLong(ExifTag.JPEGInterchangeFormatLength),
            ExifTagValue.CodingMethods => new ExifLong(ExifTag.CodingMethods),
            ExifTagValue.PixelXDimension => new ExifNumber(ExifTag.PixelXDimension),

            _ => throw new NotSupportedException(),
        };
    }
}
