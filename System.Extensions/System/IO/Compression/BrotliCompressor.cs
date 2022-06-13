namespace System.IO.Compression
{
    internal class BrotliCompressor : Compressor
    {
        public override Stream Compress(Stream Stream) {
            return new BrotliStream(Stream, CompressionLevel.Optimal);
        }

        public override Stream Decompress(Stream Stream) {
            return new BrotliStream(Stream, CompressionMode.Decompress);
        }

    }
}
