namespace System.IO.Compression
{
    public static class Compressors
    {
        public static Compressor ZLib { get; } = new ZLibCompressor();
    }
}
