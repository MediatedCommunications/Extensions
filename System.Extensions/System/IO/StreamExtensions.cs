using System.Diagnostics;
using System.Linq;

namespace System.IO
{
    public static class StreamExtensions {

        public static MemoryStream ToMemoryStream(this Stream This) {
            var ret = new MemoryStream();
            This.CopyTo(ret);
            ret.Position = 0;
            return ret;
        }

        public static StreamReader ToStreamReader(this Stream This) {
            return new StreamReader(This);
        }

        public static StreamWriter ToStreamWriter(this Stream This) {
            return new StreamWriter(This);
        }

        public static TemporaryFile ToTemporaryFile(this Func<Stream> This, bool? Lock = default, string? Tag = default) {
            using var FS = This();

            var ret = FS.ToTemporaryFile(Lock, Tag);

            return ret;
        }

        public static TemporaryFile ToTemporaryFile(this Stream This, bool? Lock = default, string? Tag = default) {
            var ret = TemporaryFile.Create(Lock: Lock, Tag:Tag);

            {
                using var FS = ret.OpenWrite();
                This.CopyTo(FS);
            }

            return ret;
        }

        public static byte[] ReadAllBytes(this Stream stream) {
            long originalPosition = 0;

            if (stream.CanSeek) {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try {
                var InitialBufferSize = 4096;
                try {
                    if (stream.CanSeek) {
                        InitialBufferSize = (int)stream.Length;
                    }
                } catch(Exception ex) {
                    ex.Ignore();
                }


                var readBuffer = new byte[InitialBufferSize];

                var totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0) {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length) {
                        var nextByte = stream.ReadByte();
                        if (nextByte != -1) {
                            var temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                var buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead) {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            } finally {
                if (stream.CanSeek) {
                    stream.Position = originalPosition;
                }
            }
        }



        private static readonly int[] CreateBufferLengths = new[] {
            CreateBuffer_Lengths_Min,
            1024 * 256,
            1024 * 1024 * 1,
            1024 * 1024 * 5,
            CreateBuffer_Lengths_Max,
        };

        private const int CreateBuffer_Lengths_Min = 1024;
        private const int CreateBuffer_Lengths_Max = 1024 * 1024 * 25;
        private const int CreateBuffer_Lengths_Step = CreateBuffer_Lengths_Max;

        private static int CalculateBufferSize(int IdealLength) {
            var ret = IdealLength;

            var Length = (
                from x in CreateBufferLengths
                where x >= IdealLength
                select x
                ).Take(1).ToList();

            if (Length.Count == 0) {

                ret = (IdealLength / CreateBuffer_Lengths_Step) * CreateBuffer_Lengths_Step;
                ret += IdealLength % CreateBuffer_Lengths_Step == 0
                    ? 0
                    : CreateBuffer_Lengths_Step
                    ;

            } else {
                ret = Length.FirstOrDefault();
            }

            if (ret < IdealLength) {
                //WARNING!  SOMEHOW WE DID MATH WRONG!
                Debugger2.BreakIfAttached();
            }

            return ret;
        }


        public static async Task<int> CountBlocksAsync(this Stream This, int BlockSize) {
            var ret = 0;

            var Size = This.Length;

            var Chunks = (int)Math.Ceiling((double)Size / (double)BlockSize);
            if (Chunks > 0) {
                ret = Chunks;
            }

            return ret;
        }

        /// <summary>
        /// This method works to read the specified amount of data from a stream.
        /// Reading from a stream doesn't always return the amount you want and this fixes that.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="BlockSize"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReadBlockAsync(this Stream This, int BlockSize) {

            var BufferSize = CalculateBufferSize(BlockSize);
            var Buffer = new byte[BufferSize];

            var StartIndex = 0;
            var Remaining = BlockSize;
            var Actual = 0;

            while (Remaining > 0) {

                var NewRead = await This
                    .ReadAsync(Buffer, StartIndex, Remaining)
                    .DefaultAwait()
                    ;

                Actual += NewRead;

                StartIndex += NewRead;
                Remaining -= NewRead;

                if(NewRead == 0) {
                    break;
                }
            }


            var ret = Buffer[0..Actual];

            return ret;
        }

    }
}
