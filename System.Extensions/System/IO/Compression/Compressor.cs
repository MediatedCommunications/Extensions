namespace System.IO.Compression
{
    public abstract class Compressor
    {
        public abstract byte[] Compress(byte[] Value);
        public abstract byte[] Decompress(byte[] Value);
    }
}
