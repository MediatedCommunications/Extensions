namespace System.Text {
    internal class Base64ByteEncoder_Formatted : ByteEncoder {

        public override string Encode(ReadOnlySpan<byte> Input) {
            return Base64Encoding.ConvertToStringFormatted(Input);
        }

        public override byte[] Decode(string Input) {
            return Base64Encoding.ConvertFromStringFormatted(Input);
        }

    }
}
