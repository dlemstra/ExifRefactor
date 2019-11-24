using System;

namespace ExifRefactor
{
    public interface IExifTag : IEquatable<IExifTag>
    {
        ushort Value { get; }
    }
}
