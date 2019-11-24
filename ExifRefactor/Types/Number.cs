using System;

namespace ExifRefactor
{
    public struct Number : IComparable<Number>
    {
        private readonly uint _value;

        public Number(uint value) => this._value = value;

        public static bool operator >(Number left, Number right) => left.CompareTo(right) == 1;

        public static bool operator <(Number left, Number right) => left.CompareTo(right) == -1;

        public static bool operator >=(Number left, Number right) => left.CompareTo(right) >= 0;

        public static bool operator <=(Number left, Number right) => left.CompareTo(right) <= 0;

        public static implicit operator Number(int value) => new Number((uint)value);

        public static implicit operator Number(uint value) => new Number(value);

        public static implicit operator Number(short value) => new Number((uint)value);

        public static implicit operator Number(ushort value) => new Number(value);

        public static explicit operator uint(Number number) => number._value;

        public static explicit operator ushort(Number number) => (ushort)number._value;

        public int CompareTo(Number other) => _value.CompareTo(other._value);
    }
}
