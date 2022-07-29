namespace System.Text {
    internal class Base64ByteEncoder_NotFormatted : ByteEncoder {

        public override string Encode(ReadOnlySpan<byte> Input) {
            return Base64Encoding.ConvertToString(Input);
        }

        public override byte[] Decode(string Input) {
            return Base64Encoding.ConvertFromString(Input);
        }

    }
}
