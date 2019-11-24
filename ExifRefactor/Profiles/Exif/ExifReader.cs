﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExifRefactor
{
    internal sealed class ExifReader
    {
        private readonly Collection<ExifTag> _invalidTags = new Collection<ExifTag>();

        private EndianReader _reader;
        private bool _isLittleEndian;
        private uint _exifOffset;
        private uint _gpsOffset;
        private uint _startIndex = 0;

        private delegate TDataType ReadMethod<TDataType>();

        public uint ThumbnailLength { get; private set; }

        public uint ThumbnailOffset { get; private set; }

        public IEnumerable<ExifTag> InvalidTags => _invalidTags;

        public Collection<ExifValue> Read(byte[] data)
        {
            var result = new Collection<ExifValue>();

            if (data == null || data.Length == 0)
                return result;

            _reader = new EndianReader(data);

            if (_reader.ReadString(4) == "Exif")
            {
                if (_reader.ReadShortMSB() != 0)
                    return result;

                _startIndex = 6;
            }

            _isLittleEndian = _reader.ReadString(2) == "II";

            if (ReadShort() != 0x002A)
                return result;

            var ifdOffset = ReadLong();
            AddValues(result, ifdOffset);

            var thumbnailOffset = ReadLong();
            ReadThumbnail(thumbnailOffset);

            if (_exifOffset != 0)
                AddValues(result, _exifOffset);

            if (_gpsOffset != 0)
                AddValues(result, _gpsOffset);

            return result;
        }

        private static TDataType[] ReadArray<TDataType>(uint numberOfComponents, ReadMethod<TDataType> read)
        {
            var result = new TDataType[numberOfComponents];

            for (int i = 0; i < numberOfComponents; i++)
            {
                result.SetValue(read(), i);
            }

            return result;
        }

        private void AddValues(Collection<ExifValue> values, uint index)
        {
            _reader.Seek(_startIndex + index);
            var count = ReadShort();

            for (ushort i = 0; i < count; i++)
            {
                var value = CreateValue();
                if (value == null)
                    continue;

                var duplicate = false;
                foreach (var val in values)
                {
                    if (val == value)
                    {
                        duplicate = true;
                        break;
                    }
                }

                if (duplicate)
                    continue;

                if (value == ExifTag.SubIFDOffset)
                    _exifOffset = ((ExifLong)value).Value;
                else if (value == ExifTag.GPSIFDOffset)
                    _gpsOffset = ((ExifLong)value).Value;
                else
                    values.Add(value);
            }
        }

        private ExifValue CreateValue()
        {
            if (!_reader.CanRead(12))
                return null;

            var tagValue = (ExifTagValue)ReadShort();
            var tag = ExifTag.Get(tagValue);
            var dataType = EnumHelper.Parse(ReadShort(), ExifDataType.Unknown);
            ExifValue value = null;

            if (dataType == ExifDataType.Unknown)
                return null;

            var numberOfComponents = ReadLong();

            if (dataType == ExifDataType.Undefined && numberOfComponents == 0)
                numberOfComponents = 4;

            if (tag == null)
            {
                if (numberOfComponents == 1)
                    _invalidTags.Add(new ExifTag<byte>(tagValue));
                else
                    _invalidTags.Add(new ExifTag<byte[]>(tagValue));

                return null;
            }

            var oldIndex = _reader.Index;
            var length = numberOfComponents * ExifDataTypes.GetSize(dataType);

            if (length <= 4)
            {
                value = CreateValue(tag, dataType, numberOfComponents);
            }
            else
            {
                var newIndex = _startIndex + ReadLong();

                if (_reader.Seek(newIndex))
                {
                    if (_reader.CanRead(length))
                    {
                        value = CreateValue(tag, dataType, numberOfComponents);
                    }
                }

                if (value == null)
                {
                    _invalidTags.Add(tag);
                }
            }

            _reader.Seek(oldIndex + 4);

            return value;
        }

        private ExifValue CreateValue(ExifTag tag, ExifDataType dataType, uint numberOfComponents)
        {
            var exifValue = ExifValues.Create(tag);
            if (exifValue == null)
            {
                return null;
            }

            var value = ReadValue(dataType, numberOfComponents);
            if (!exifValue.TrySetValue(value))
            {
                return null;
            }

            return exifValue;
        }

        private object ReadValue(ExifDataType dataType, uint numberOfComponents)
        {
            switch (dataType)
            {
                case ExifDataType.Byte:
                case ExifDataType.Undefined:
                    if (numberOfComponents == 1)
                        return ReadByte();
                    else
                        return ReadArray(numberOfComponents, ReadByte);

                case ExifDataType.Double:
                    if (numberOfComponents == 1)
                        return ReadDouble();
                    else
                        return ReadArray(numberOfComponents, ReadDouble);

                case ExifDataType.Float:

                    if (numberOfComponents == 1)
                        return ReadFloat();
                    else
                        return ReadArray(numberOfComponents, ReadFloat);

                case ExifDataType.Long:
                    if (numberOfComponents == 1)
                        return ReadLong();
                    else
                        return ReadArray(numberOfComponents, ReadLong);

                case ExifDataType.Rational:
                    if (numberOfComponents == 1)
                        return ReadRational();
                    else
                        return ReadArray(numberOfComponents, ReadRational);

                case ExifDataType.Short:
                    if (numberOfComponents == 1)
                        return ReadShort();
                    else
                        return ReadArray(numberOfComponents, ReadShort);

                case ExifDataType.SignedByte:
                    if (numberOfComponents == 1)
                        return ReadSignedByte();
                    else
                        return ReadArray(numberOfComponents, ReadSignedByte);

                case ExifDataType.SignedLong:
                    if (numberOfComponents == 1)
                        return ReadSignedLong();
                    else
                        return ReadArray(numberOfComponents, ReadSignedLong);

                case ExifDataType.SignedRational:
                    if (numberOfComponents == 1)
                        return ReadSignedRational();
                    else
                        return ReadArray(numberOfComponents, ReadSignedRational);

                case ExifDataType.SignedShort:
                    if (numberOfComponents == 1)
                        return ReadSignedShort();
                    else
                        return ReadArray(numberOfComponents, ReadSignedShort);

                case ExifDataType.String:
                    return ReadString(numberOfComponents);

                default:
                    throw new NotSupportedException();
            }
        }

        private byte ReadByte() => _reader.ReadByte() ?? 0;

        private double ReadDouble() => (_isLittleEndian ? _reader.ReadDoubleLSB() : _reader.ReadDoubleMSB()) ?? 0;

        private float ReadFloat() => (_isLittleEndian ? _reader.ReadFloatLSB() : _reader.ReadFloatMSB()) ?? 0;

        private uint ReadLong() => (_isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB()) ?? 0;

        private ushort ReadShort() => (_isLittleEndian ? _reader.ReadShortLSB() : _reader.ReadShortMSB()) ?? 0;

        private string ReadString(uint length) => _isLittleEndian ? _reader.ReadString(length) : _reader.ReadString(length);

        private Rational ReadRational()
        {
            var numerator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (numerator == null)
                return default;

            var denominator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (denominator == null)
                return default;

            return new Rational(numerator.Value, denominator.Value, false);
        }

        private unsafe SignedRational ReadSignedRational()
        {
            var numerator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (numerator == null)
                return default;

            var denominator = _isLittleEndian ? _reader.ReadLongLSB() : _reader.ReadLongMSB();
            if (denominator == null)
                return default;

            var num = numerator.Value;
            var dem = denominator.Value;

            return new SignedRational(*(int*)&num, *(int*)&dem, false);
        }

        private unsafe sbyte ReadSignedByte()
        {
            var result = ReadByte();
            return *(sbyte*)&result;
        }

        private unsafe int ReadSignedLong()
        {
            var result = ReadLong();
            return *(int*)&result;
        }

        private unsafe short ReadSignedShort()
        {
            var result = ReadShort();
            return *(short*)&result;
        }

        private void ReadThumbnail(uint offset)
        {
            var values = new Collection<ExifValue>();
            AddValues(values, offset);

            foreach (var value in values)
            {
                if (value == ExifTag.JPEGInterchangeFormat)
                    ThumbnailOffset = ((ExifLong)value).Value + _startIndex;
                else if (value == ExifTag.JPEGInterchangeFormatLength)
                    ThumbnailLength = ((ExifLong)value).Value;
            }
        }
    }
}
