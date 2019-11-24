namespace ExifRefactor
{
    internal enum ExifTagValue : ushort
    {
        Unknown = 0xFFFF,

        SubIFDOffset = 0x8769,

        GPSIFDOffset = 0x8825,

        [ExifTagDescription(0U, "Unspecified compression")]
        [ExifTagDescription(1U, "Modified Huffman")]
        [ExifTagDescription(2U, "Modified Read")]
        [ExifTagDescription(4U, "Modified MR")]
        [ExifTagDescription(8U, "JBIG")]
        [ExifTagDescription(16U, "Baseline JPEG")]
        [ExifTagDescription(32U, "JBIG color")]
        CodingMethods = 0x0193,

        JPEGInterchangeFormat = 0x0201,

        JPEGInterchangeFormatLength = 0x0202,

        PixelXDimension = 0xA002,
    }
}
