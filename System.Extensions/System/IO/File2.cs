using System.Linq;

namespace System.IO
{
    [Flags]
    public enum TransferOptions {
        None = 0,
        Copy = 1,
        DeleteSource = 2,
        Move = Copy | DeleteSource,
        OverwriteDest = 4,
        CreateDestPath = 8,
    }

    public static class File2 {

        public static void Transfer(string SourceFile, string DestFile, params TransferOptions[] OptionList) {
            var Options = TransferOptions.None;
            foreach (var item in OptionList)
            {
                Options |= item;
            }

            
            var Move = false;
            var Copy = false;
            var Overwrite = false;

            var DeleteSource = true;

            if (Options.HasFlag(TransferOptions.Move)) {
                Move = true;
            }

            if (Options.HasFlag(TransferOptions.Copy))
            {
                Copy = true;
            }

            if (Options.HasFlag(TransferOptions.DeleteSource))
            {
                DeleteSource = true;
            }

            if (Options.HasFlag(TransferOptions.OverwriteDest)) {
                Overwrite = true;
            }

            if (Options.HasFlag(TransferOptions.CreateDestPath)) {
                var Folder = DestFile.Parse().AsPath().Directory;

                Directory.CreateDirectory(Folder);
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
