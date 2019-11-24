namespace ExifRefactor
{
    internal static class ExifTags
    {
        public static ExifParts GetPart(IExifTag tag)
        {
            switch ((ExifTagValue)tag.Value)
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
