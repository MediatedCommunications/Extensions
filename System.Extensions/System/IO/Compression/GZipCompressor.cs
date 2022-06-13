namespace System.IO.Compression
{
    internal class GZipCompressor : Compressor
    {
        public override Stream Compress(Stream Stream) {
            return new GZipStream(Stream, CompressionLevel.Optimal);
        }

        public override Stream Decompress(Stream Stream) {
            return new GZipStream(Stream, CompressionMode.Decompress);
        }

    }
}
