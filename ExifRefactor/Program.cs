using System;

namespace ExifRefactor
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var profile = new ExifProfile();
            profile.SetValue(ExifTag.PixelXDimension, 10);

            var data = profile.ToByteArray();
            profile = new ExifProfile(data);

            Console.WriteLine(profile.GetValue(ExifTag.PixelXDimension));

            profile.SetValue(ExifTag.PixelXDimension, 65536);
            profile.SetValue(ExifTag.CodingMethods, 2U);

            data = profile.ToByteArray();
            profile = new ExifProfile(data);

            Console.WriteLine();
            Console.WriteLine(profile.GetValue(ExifTag.PixelXDimension));
            Console.WriteLine(profile.GetValue(ExifTag.CodingMethods));

            Console.ReadLine();
        }
    }
}
