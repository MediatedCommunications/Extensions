namespace System.IO.Compression
{
    internal class DeflateCompressor : Compressor
    {
        public override Stream Compress(Stream Stream) {
            return new DeflateStream(Stream, CompressionLevel.Optimal);
        }

        public override Stream Decompress(Stream Stream) {
            return new DeflateStream(Stream, CompressionMode.Decompress);
        }

    }
}
