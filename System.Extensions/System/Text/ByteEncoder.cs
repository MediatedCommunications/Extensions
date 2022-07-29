namespace System.Text {
    public abstract class ByteEncoder : IByteEncoder, IByteDecoder {
        public abstract string Encode(ReadOnlySpan<byte> Input);

        public abstract byte[] Decode(string Input);
    }

    public interface IByteEncoder {
        public abstract string Encode(ReadOnlySpan<byte> Input);
    }

    public interface IByteDecoder {
        public abstract byte[] Decode(string Input);
    }
}
