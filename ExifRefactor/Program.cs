﻿using System;

namespace ExifRefactor
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var profile = new ExifProfile();
            profile.SetValue(ExifTag.PixelXDimension, 10);
            profile.SetValue(ExifTag.CodingMethods, 2U);

            var data = profile.ToByteArray();
            profile = new ExifProfile(data);

            Console.WriteLine(profile.GetValue(ExifTag.PixelXDimension));
            Console.WriteLine(profile.GetValue(ExifTag.CodingMethods));

            profile.SetValue(ExifTag.CodingMethods, 4U);
            foreach (var value in profile.Values)
            {
                if (value == ExifTag.PixelXDimension)
                {
                    value.TrySetValue(65536);
                }
            }

            data = profile.ToByteArray();
            profile = new ExifProfile(data);

            Console.WriteLine();
            Console.WriteLine(profile.GetValue(ExifTag.PixelXDimension));
            Console.WriteLine(profile.GetValue(ExifTag.CodingMethods));

            Console.ReadLine();
        }
    }
}
