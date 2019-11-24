using System;

namespace ExifRefactor
{
    /// <summary>
    /// Specifies which parts will be written when the profile is added to an image.
    /// </summary>
    [Flags]
    public enum ExifParts
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// IfdTags
        /// </summary>
        IfdTags = 1,

        /// <summary>
        /// ExifTags
        /// </summary>
        ExifTags = 4,

        /// <summary>
        /// GpsTags
        /// </summary>
        GpsTags = 8,

        /// <summary>
        /// All
        /// </summary>
        All = IfdTags | ExifTags | GpsTags,
    }
}
