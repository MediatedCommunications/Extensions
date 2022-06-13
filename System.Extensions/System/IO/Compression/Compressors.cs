namespace System.IO.Compression
{
    public static class Compressors
    {
        public static Compressor Brotli { get; } 
        public static Compressor Deflate { get; }
        public static Compressor GZip { get; } 
        public static Compressor ZLib { get; }

        static Compressors() {
            Brotli = new BrotliCompressor();
            Deflate = new DeflateCompressor();
            GZip = new GZipCompressor();
            ZLib = new ZLibCompressor();
        }

    }
}
