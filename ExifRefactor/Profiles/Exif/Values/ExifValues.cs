﻿using System;

namespace ExifRefactor
{
    public static partial class ExifValues
    {
        internal static ExifValue Create(IExifTag tag) => (ExifValue)CreateValue(tag);

        internal static ExifValue<TValueType> Create<TValueType>(ExifTag<TValueType> tag) => (ExifValue<TValueType>)CreateValue(tag);

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
