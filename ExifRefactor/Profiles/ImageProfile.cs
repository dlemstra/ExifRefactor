namespace ExifRefactor
{
    public class ImageProfile
    {
        public ImageProfile(byte[] data)
        {
            Data = data;
        }

        protected byte[] Data { get; private set; }

        public byte[] ToByteArray()
        {
            Data = UpdateData();
            return Copy(Data);
        }

        protected virtual byte[] UpdateData() => Data;

        private static byte[] Copy(byte[] data)
        {
            if (data == null || data.Length == 0)
                return new byte[0];

            var result = new byte[data.Length];
            data.CopyTo(result, 0);
            return result;
        }
    }
}
