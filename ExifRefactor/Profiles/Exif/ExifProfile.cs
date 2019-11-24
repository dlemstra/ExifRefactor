using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExifRefactor
{

    public sealed class ExifProfile : ImageProfile
    {
        private Collection<ExifValue> _values;
        private List<IExifTag> _invalidTags = new List<IExifTag>();

        public ExifProfile() : base(null)
        {
        }

        public ExifProfile(byte[] data) : base(data)
        {
        }

        public uint ThumbnailOffset { get; private set; }

        public uint ThumbnailLength { get; private set; }

        public ExifParts Parts { get; set; } = ExifParts.All;

        public IEnumerable<IExifTag> InvalidTags
        {
            get
            {
                InitializeValues();
                return _invalidTags;
            }
        }

        public IEnumerable<ExifValue> Values
        {
            get
            {
                InitializeValues();
                return _values;
            }
        }

        public ExifValue<TValueType> GetValue<TValueType>(ExifTag<TValueType> tag)
        {
            InitializeValues();

            foreach (var exifValue in _values)
            {
                if (exifValue == tag)
                {
                    return (ExifValue<TValueType>)exifValue;
                }
            }

            return null;
        }

        public void SetValue<TValueType>(ExifTag<TValueType> tag, TValueType value)
        {
            var currentValue = GetValue(tag);
            if (currentValue != null)
            {
                currentValue.Value = value;
                return;
            }

            var newExifValue = ExifValues.Create(tag);
            newExifValue.Value = value;

            _values.Add(newExifValue);
        }

        protected override byte[] UpdateData()
        {
            if (_values == null)
            {
                return Data;
            }

            if (_values.Count == 0)
            {
                return null;
            }

            var writer = new ExifWriter(Parts);
            return writer.Write(_values);
        }

        private void InitializeValues()
        {
            if (_values != null)
                return;

            if (Data == null)
            {
                _values = new Collection<ExifValue>();
                return;
            }

            var reader = new ExifReader();
            _values = reader.Read(Data);
            _invalidTags.AddRange(reader.InvalidTags);
            ThumbnailOffset = reader.ThumbnailOffset;
            ThumbnailLength = reader.ThumbnailLength;
        }
    }
}
