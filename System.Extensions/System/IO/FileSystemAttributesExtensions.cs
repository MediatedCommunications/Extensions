namespace System.IO {
    public static class FileSystemAttributesExtensions {

        private static FileSystemAttributes HasFlag(this FileAttributes This, FileAttributes Flag, FileSystemAttributes True) {
            return HasFlag(This, Flag, True, FileSystemAttributes.None);
        }

        private static FileSystemAttributes HasFlag(this FileAttributes This, FileAttributes Flag, FileSystemAttributes True, FileSystemAttributes False) {
            var ret = This.HasFlag(Flag) ? True : False;

            return ret;
        }

        public static FileSystemAttributes ComputeNormal(this FileSystemAttributes This) {
            var ret = This;
            var IsNormal =
                !( false
                || This.HasFlag(FileSystemAttributes.Hidden)
                || This.HasFlag(FileSystemAttributes.System)
                || This.HasFlag(FileSystemAttributes.ReadOnly)
                || This.HasFlag(FileSystemAttributes.Archive)

                || This.HasFlag(FileSystemAttributes.SystemGenerated)
                || This.HasFlag(FileSystemAttributes.Executable)

                || This.HasFlag(FileSystemAttributes.Temporary)
                || This.HasFlag(FileSystemAttributes.Compressed)
                || This.HasFlag(FileSystemAttributes.Encrypted)
                || This.HasFlag(FileSystemAttributes.Offline)
                || This.HasFlag(FileSystemAttributes.Empty)
                );

            if (IsNormal) {
                ret |= FileSystemAttributes.Normal;
            } else {
                ret &= ~FileSystemAttributes.Normal;
            }

            return ret;
        }

        public static FileSystemAttributes ToFileSystemAttributes(this FileAttributes This) {
            var ret = FileSystemAttributes.None;
            ret |= This.HasFlag(FileAttributes.Hidden, FileSystemAttributes.Hidden);
            ret |= This.HasFlag(FileAttributes.System, FileSystemAttributes.System);
            ret |= This.HasFlag(FileAttributes.ReadOnly, FileSystemAttributes.ReadOnly);
            ret |= This.HasFlag(FileAttributes.Archive, FileSystemAttributes.Archive);
            ret |= This.HasFlag(FileAttributes.Temporary, FileSystemAttributes.Temporary);
            ret |= This.HasFlag(FileAttributes.Compressed, FileSystemAttributes.Compressed);
            ret |= This.HasFlag(FileAttributes.Encrypted, FileSystemAttributes.Encrypted);
            ret |= This.HasFlag(FileAttributes.Offline, FileSystemAttributes.Offline);

            ret |= This.HasFlag(FileAttributes.SparseFile, FileSystemAttributes.SparseFile);
            ret |= This.HasFlag(FileAttributes.ReparsePoint, FileSystemAttributes.ReparsePoint);
            ret |= This.HasFlag(FileAttributes.NotContentIndexed, FileSystemAttributes.NotContentIndexed);
            ret |= This.HasFlag(FileAttributes.IntegrityStream, FileSystemAttributes.IntegrityStream);
            ret |= This.HasFlag(FileAttributes.NoScrubData, FileSystemAttributes.NoScrubData);

            ret |= This.HasFlag(FileAttributes.Directory, FileSystemAttributes.Directory);
            ret |= This.HasFlag(FileAttributes.Device, FileSystemAttributes.Device);

            //File
            ret |= (true
                && !This.HasFlag(FileAttributes.Device) 
                && !This.HasFlag(FileAttributes.Directory) 
                ? FileSystemAttributes.File 
                : FileSystemAttributes.None
                )
                ;

            ret = ret.ComputeNormal();

            return ret;
        }
    }

}
