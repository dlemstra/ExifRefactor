namespace ExifRefactor
{
    internal static class ExifTags
    {
        public static ExifParts GetPart(ExifTag tag)
        {
            switch ((ExifTagValue)(ushort)tag)
            {
                case ExifTagValue.CodingMethods:
                case ExifTagValue.JPEGInterchangeFormat:
                case ExifTagValue.JPEGInterchangeFormatLength:
                    return ExifParts.IfdTags;

                case ExifTagValue.PixelXDimension:
                    return ExifParts.ExifTags;

                default:
                    return ExifParts.None;
            }
        }
    }
}
