using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.IO
{

    public class TemporaryFile : DisplayClass, IDisposable {
        public string FullPath { get; private set; }

        public DateTimeOffset CreationTime { get; private set; }
        public DateTimeOffset? LastAccessTime { get; private set; }
        public DateTimeOffset? LastReadTime { get; private set; }
        public DateTimeOffset? LastWriteTime { get; private set; }
        public bool HasChanged { get; private set; }

        protected Stream? LockStream { get; private set; }
        protected bool AlreadyDisposed { get; private set; }

        public FileInfo Info() {
            return new FileInfo(FullPath);
        }

        public long Size {
            get {
                var ret = 0L;

                var FI = Info();
                if (FI.Exists) {
                    ret = FI.Length;
                }

                return ret;
            }
        }

        public TemporaryFile(string FullPath, bool? Lock = default) {
            this.FullPath = FullPath;

            LockAccess();

            var ShouldLock = Lock ?? false;

            if (!ShouldLock) {
                UnlockAccess();
            }

        }

        ~TemporaryFile() {
            Dispose(false);
        }


        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool Disposing) {
            if (AlreadyDisposed) {
                return;
            }

            FreeManaged();
            FreeUnmanaged();

            AlreadyDisposed = true;
        }

        private void FreeManaged() {
        }

        private void FreeUnmanaged() {

            var ToRemove = Instances.ToList();
            foreach (var item in ToRemove) {
                try {
                    item.Key.Dispose();
                } catch { }
            }

            try {
                File.Delete(FullPath);
                
                
            } catch (Exception ex) {
                ex.Ignore();
            }
        }

        private void LockAccess() {
            if (this.LockStream is null) {
                this.LockStream = OpenLock();
            }
        }

        private void UnlockAccess() {
            if(this.LockStream is { } V1) {
                V1.Close();
                this.LockStream = default;
            }
        }

        public static TemporaryFile CreateLocked(string FullPath) {
            return Create(FullPath, true);
        }


        public static TemporaryFile Create(string FullPath, bool? Lock = default) {
            return new TemporaryFile(FullPath, Lock);
        }

        public static TemporaryFile CreateLocked(string? FolderName = default, string? FileName = default, string? Extension = default) {
            return Create(FolderName, FileName, Extension, true);
        }

        public static TemporaryFile Create(System.SpecialFolder? Folder, string? FileName = default, string? Extension = default, bool? Lock = default) {
            var FolderName = (Folder ?? System.SpecialFolders.Local.CommonDocuments).GetPath();

            return Create(FolderName, FileName, Extension, Lock);
        }

        public static TemporaryFile Create(string? FolderName = default, string? FileName = default, string? Extension = default, bool? Lock = default) {
            var ActualFolderName = new[] { FolderName, Path.GetTempPath() }.WhereIsNotBlank().Coalesce();
            var ActualFileName = new[] { FileName, $@"{Guid.NewGuid()}" }.WhereIsNotBlank().Coalesce();
            var ActualExtension = new[] { Extension }.Coalesce();

            var DestPath = ActualFolderName.Parse().AsPath()
                .Combine(ActualFileName)
                .WithExtension(ActualExtension)
                .FullPath
                ;

            return Create(DestPath, Lock);
        }

        public IEnumerable<TemporaryFileStream> ActiveStreams() {
            foreach (var item in Instances) {
                yield return item.Value;
            }
        }

        private ConcurrentDictionary<TemporaryFileStream, TemporaryFileStream> Instances = new();

        private TemporaryFileStream AddStream(TemporaryFileStream S) {
            var ret = S;

            Instances[S] = S;

            return ret;
        }

        private void RemoveStream(TemporaryFileStream S) {
            Instances.TryRemove(S, out _);
        }

        private void OnClose(TemporaryFileStream S) {
            RemoveStream(S);
        }

        private void OnRead(TemporaryFileStream S) {
            var Now = DateTimeOffset.Now;

            this.LastAccessTime = Now;
            this.LastReadTime = Now;
        }

        private void OnWrite(TemporaryFileStream S) {
            var Now = DateTimeOffset.Now;

            this.LastAccessTime = Now;
            this.LastWriteTime = Now;
            
            this.HasChanged = true;
        }



        private FileShare GetShare(FileShare Preferred) {
            var ret = this.LockStream == null
                ? Preferred
                : FileShare.ReadWrite
                ;

            return ret;
        }


        private TemporaryFileStream OpenInternal(FileMode Mode, FileAccess Access, FileShare Share) {
            return AddStream(new TemporaryFileStream(this, OnRead, OnWrite, OnClose, Mode, Access, Share));
        }

        private TemporaryFileStream OpenLock() {
            return OpenInternal(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        }

        public TemporaryFileStream OpenRead() {
            return OpenInternal(FileMode.Open, FileAccess.Read, GetShare(FileShare.Read));
        }

        public TemporaryFileStream OpenWrite() {
            return OpenInternal(FileMode.OpenOrCreate, FileAccess.Write, GetShare(FileShare.None));
        }

        public TemporaryFileStream Open(FileMode mode) {
            return OpenInternal(mode, mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite, GetShare(FileShare.None));
        }

        public TemporaryFileStream Open(FileMode mode, FileAccess access) {
            return OpenInternal(mode, access, GetShare(FileShare.None));
        }

        public TemporaryFileStream Open(FileMode mode, FileAccess access, FileShare share) {
            return OpenInternal(mode, access, GetShare(share));
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(FullPath)
                ;
        }


    }


}
