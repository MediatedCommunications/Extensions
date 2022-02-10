using System.Linq;

namespace System.IO
{
    [Flags]
    public enum TransferOptions {
        None = 0,
        Copy = 1,
        Source_Delete = 2,
        CreatePath = 4,

        IfDestExists_Overwrite = 128,
        IfDestExists_MoveToTemp = 256,

        Move = Copy | Source_Delete,
        TryOverwrite = IfDestExists_Overwrite,
        ForceOverwrite = IfDestExists_Overwrite | IfDestExists_MoveToTemp,

        ForceCopy = CreatePath | Copy | ForceOverwrite
        
    }

    public static class File2 {

        public static void Move(string SourceFile, string DestFile, bool Overwrite = true, bool CreatePath = true) {
            var Options = TransferOptions.None
                | TransferOptions.Move
                ;

            if (Overwrite) {
                Options |= TransferOptions.IfDestExists_Overwrite;
            }

            if (CreatePath) {
                Options |= TransferOptions.CreatePath;
            }

            Transfer(SourceFile, DestFile, Options);

        }

        public static void Copy(string SourceFile, string DestFile, bool Overwrite = true, bool CreatePath = true) {
            var Options = TransferOptions.None
                | TransferOptions.Copy
                ;

            if (Overwrite) {
                Options |= TransferOptions.IfDestExists_Overwrite;
            }

            if (CreatePath) {
                Options |= TransferOptions.CreatePath;
            }

            Transfer(SourceFile, DestFile, Options);

        }

        public static void Transfer(string SourceFile, string DestFile, params TransferOptions[] OptionList) {
            var Options = TransferOptions.None;
            foreach (var item in OptionList)
            {
                Options |= item;
            }

            
            var Move = false;
            var Copy = false;
            var Overwrite = false;

            var DeleteSource = false;

            if (Options.HasFlag(TransferOptions.Move)) {
                Move = true;
            }

            if (Options.HasFlag(TransferOptions.Copy))
            {
                Copy = true;
            }

            if (Options.HasFlag(TransferOptions.Source_Delete))
            {
                DeleteSource = true;
            }

            if (Options.HasFlag(TransferOptions.IfDestExists_Overwrite)) {
                Overwrite = true;
            }

            if (Options.HasFlag(TransferOptions.CreatePath)) {
                var Folder = DestFile.Parse().AsPath().Directory;

                Directory.CreateDirectory(Folder);
            }

            if(Options.HasFlag(TransferOptions.IfDestExists_MoveToTemp) && System.IO.File.Exists(DestFile)) {
                using var TFS = TemporaryFile.Create();
                System.IO.File.Move(DestFile, TFS.FullPath, true);
            }

            if (Move) {
                File.Move(SourceFile, DestFile, Overwrite);
            } else if(Copy) {
                File.Copy(SourceFile, DestFile, Overwrite);
            } else if (DeleteSource)
            {
                File.Delete(SourceFile);
            }


        }



    }
}
