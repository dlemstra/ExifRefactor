﻿using System;
using System.Text;

namespace ExifRefactor
{
    internal struct BigRational : IEquatable<BigRational>
    {
        public BigRational(long numerator, long denominator)
          : this(numerator, denominator, false)
        {
        }

        public BigRational(long numerator, long denominator, bool simplify)
        {
            Numerator = numerator;
            Denominator = denominator;

            if (simplify)
                Simplify();
        }

        public BigRational(double value, bool bestPrecision)
        {
            if (double.IsNaN(value))
            {
                Numerator = Denominator = 0;
                return;
            }

            if (double.IsPositiveInfinity(value))
            {
                Numerator = 1;
                Denominator = 0;
                return;
            }

            if (double.IsNegativeInfinity(value))
            {
                Numerator = -1;
                Denominator = 0;
                return;
            }

            Numerator = 1;
            Denominator = 1;

            double val = Math.Abs(value);
            double df = Numerator / (double)Denominator;
            double epsilon = bestPrecision ? double.Epsilon : .000001;

            while (Math.Abs(df - val) > epsilon)
            {
                if (df < val)
                    Numerator++;
                else
                {
                    Denominator++;
                    Numerator = (int)(val * Denominator);
                }

                df = Numerator / (double)Denominator;
            }

            if (value < 0.0)
                Numerator *= -1;

            Simplify();
        }

        public long Denominator { get; private set; }

        public long Numerator { get; private set; }

        private bool IsIndeterminate
        {
            get
            {
                if (Denominator != 0)
                    return false;

                return Numerator == 0;
            }
        }

        private bool IsInteger => Denominator == 1;

        private bool IsNegativeInfinity
        {
            get
            {
                if (Denominator != 0)
                    return false;

                return Numerator == -1;
            }
        }

        private bool IsPositiveInfinity
        {
            get
            {
                if (Denominator != 0)
                    return false;

                return Numerator == 1;
            }
        }

        private bool IsZero
        {
            get
            {
                if (Denominator != 1)
                    return false;

                return Numerator == 0;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BigRational))
                return false;

            return Equals((BigRational)obj);
        }

        public bool Equals(BigRational other)
        {
            if (Denominator == other.Denominator)
                return Numerator == other.Numerator;

            if (Numerator == 0 && Denominator == 0)
                return other.Numerator == 0 && other.Denominator == 0;

            if (other.Numerator == 0 && other.Denominator == 0)
                return Numerator == 0 && Denominator == 0;

            return (Numerator * other.Denominator) == (Denominator * other.Numerator);
        }

        public override int GetHashCode() => ((Numerator * 397) ^ Denominator).GetHashCode();

        public string ToString(IFormatProvider provider)
        {
            if (IsIndeterminate)
                return "Indeterminate";

            if (IsPositiveInfinity)
                return "PositiveInfinity";

            if (IsNegativeInfinity)
                return "NegativeInfinity";

            if (IsZero)
                return "0";

            if (IsInteger)
                return Numerator.ToString(provider);

            var sb = new StringBuilder();
            sb.Append(Numerator.ToString(provider));
            sb.Append("/");
            sb.Append(Denominator.ToString(provider));

            return sb.ToString();
        }

        private static long GreatestCommonDivisor(long a, long b) => b == 0 ? a : GreatestCommonDivisor(b, a % b);

        private void Simplify()
        {
            if (IsIndeterminate)
                return;

            if (IsNegativeInfinity)
                return;

            if (IsPositiveInfinity)
                return;

            if (IsInteger)
                return;

            if (IsZero)
                return;

            if (Numerator == 0)
            {
                Denominator = 0;
                return;
            }

            if (Numerator == Denominator)
            {
                Numerator = 1;
                Denominator = 1;
            }

            long gcd = GreatestCommonDivisor(Math.Abs(Numerator), Math.Abs(Denominator));
            if (gcd > 1)
            {
                Numerator /= gcd;
                Denominator /= gcd;
            }
        }
    }
}
