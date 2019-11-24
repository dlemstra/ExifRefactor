namespace ExifRefactor
{
    public static class ExifTag
    {
        public static ExifTag<uint> SubIFDOffset { get; } = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

        public static ExifTag<uint> GPSIFDOffset { get; } = new ExifTag<uint>(ExifTagValue.GPSIFDOffset);

        public static ExifTag<uint> CodingMethods { get; } = new ExifTag<uint>(ExifTagValue.CodingMethods);

        public static ExifTag<uint> JPEGInterchangeFormat { get; } = new ExifTag<uint>(ExifTagValue.JPEGInterchangeFormat);

        public static ExifTag<uint> JPEGInterchangeFormatLength { get; } = new ExifTag<uint>(ExifTagValue.JPEGInterchangeFormatLength);

        public static ExifTag<Number> PixelXDimension { get; } = new ExifTag<Number>(ExifTagValue.PixelXDimension);

        internal static IExifTag Get(ExifTagValue tag) => (tag) switch
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
