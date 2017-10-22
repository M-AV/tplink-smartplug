namespace tplink_smartplug
{
    public class TpLinkAutokeyCipher
    {
        private readonly byte _startKey = 171;
        private readonly byte[] _header = { 0x00, 0x00, 0x00, 0x00 };

        public byte[] Encrypt(byte[] source)
        {
            var result = new byte[_header.Length + source.Length];
            var key = _startKey;
            for (int sourceIndex = 0, resultIndex = 4; sourceIndex < source.Length; sourceIndex++, resultIndex++)
            {
                var b = (byte)(key ^ source[sourceIndex]);
                key = b;
                result[resultIndex] = b;
            }
            return result;
        }

        public byte[] Decrypt(byte[] source)
        {
            var key = _startKey;
            var result = new byte[source.Length - 4];

            for (int sourceIndex = 4, resultIndex = 0; sourceIndex < source.Length; sourceIndex++, resultIndex++)
            {
                var a = (byte)(key ^ source[sourceIndex]);
                key = source[sourceIndex];
                result[resultIndex] = a;
            }

            return result;
        }
    }
}