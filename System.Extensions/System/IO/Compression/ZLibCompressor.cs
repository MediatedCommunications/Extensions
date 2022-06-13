namespace System.IO.Compression
{
    internal class ZLibCompressor : Compressor
    {
        public override Stream Compress(Stream Stream) {
            return new ZLibStream(Stream, CompressionLevel.Optimal);
        }

        public override Stream Decompress(Stream Stream) {
            return new ZLibStream(Stream, CompressionMode.Decompress);
        }

    }
}
