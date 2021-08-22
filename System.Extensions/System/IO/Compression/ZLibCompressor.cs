namespace System.IO.Compression
{
    internal class ZLibCompressor : Compressor
    {
        public override byte[] Compress(byte[] Value)
        {
            using var MS = new MemoryStream();
            using var Stream = new ZLibStream(MS, CompressionLevel.Optimal);
            Stream.Write(Value);
            Stream.Flush();

            var ret = MS.ToArray();

            return ret;
        }

        public override byte[] Decompress(byte[] Value) {
            var MS = Value.ToMemoryStream();
            using var Stream = new ZLibStream(MS, CompressionMode.Decompress);
            var ret = Stream.ReadAllBytes();

            return ret;
        }

    }
}
