using System;

namespace ExifRefactor
{
    public static partial class ExifValues
    {
        internal static IExifValue Create(IExifTag tag) => (IExifValue)CreateValue(tag);

        internal static IExifValue<TValueType> Create<TValueType>(ExifTag<TValueType> tag) => (IExifValue<TValueType>)CreateValue(tag);

        private static object CreateValue(IExifTag tag) => ((ExifTagValue)tag.Value) switch
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
