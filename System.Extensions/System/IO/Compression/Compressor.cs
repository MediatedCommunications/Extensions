using System.Diagnostics;

namespace System.IO.Compression
{
    public abstract class Compressor : DisplayClass
    {
        public byte[] Compress(byte[] Value) {
            using var ms = new MemoryStream();
            using var Stream = Compress(ms);
            Stream.Write(Value);
            Stream.Flush();

            var ret = ms.ToArray();

            return ret;
        }

        public byte[] Decompress(byte[] Value) {
            var MS = Value.ToMemoryStream();
            using var Stream = Decompress(MS);
            var ret = Stream.ReadAllBytes();

            return ret;
        }



        public abstract Stream Compress(Stream Stream);
        public abstract Stream Decompress(Stream Stream);


    }
}
