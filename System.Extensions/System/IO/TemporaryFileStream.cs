using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
    public class TemporaryFileStream : FileStream {
        private TemporaryFile? Internals { get; set; }

        private Action<TemporaryFileStream> OnRead;
        private Action<TemporaryFileStream> OnWrite;
        private Action<TemporaryFileStream> OnClose;

        internal TemporaryFileStream(TemporaryFile Manager, Action<TemporaryFileStream> OnRead, Action<TemporaryFileStream> OnWrite, Action<TemporaryFileStream> OnClose, FileMode FileMode, FileAccess FileAccess, FileShare FileShare) : base(Manager.FullPath, FileMode, FileAccess, FileShare) {
            this.Internals = Manager;
            this.OnRead = OnRead;
            this.OnWrite = OnWrite;
            this.OnClose = OnClose;
        }


        public override void Write(byte[] array, int offset, int count) {
            OnWrite(this);
            base.Write(array, offset, count);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) {
            OnWrite(this);
            return base.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override void WriteByte(byte value) {
            OnWrite(this);
            base.WriteByte(value);
        }

        public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback? userCallback, object? stateObject) {
            OnWrite(this);
            return base.BeginWrite(array, offset, numBytes, userCallback, stateObject);
        }

        public override void EndWrite(IAsyncResult asyncResult) {
            OnWrite(this);
            base.EndWrite(asyncResult);
        }


        public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback? userCallback, object? stateObject) {
            OnRead(this);
            return base.BeginRead(array, offset, numBytes, userCallback, stateObject);
        }

        public override int Read(byte[] array, int offset, int count) {
            OnRead(this);
            return base.Read(array, offset, count);
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) {
            OnRead(this);
            return base.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override int ReadByte() {
            OnRead(this);
            return base.ReadByte();
        }

        public override int EndRead(IAsyncResult asyncResult) {
            OnRead(this);
            return base.EndRead(asyncResult);
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            if (disposing) {
                this.Internals = null;
                this.OnClose(this);
            }


        }
    }


}
