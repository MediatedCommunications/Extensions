using System.Linq;

namespace System.IO
{

    public enum TransferOptions {
        Copy,
        Move,
        Overwrite,
        CreatePath
    }

    public static class File2 {

        public static void Transfer(string SourceFile, string DestFile, params TransferOptions[] Options) {
            var Move = false;
            var Overwrite = false;

            if (Options.Contains(TransferOptions.Move)) {
                Move = true;
            }

            if (Options.Contains(TransferOptions.Overwrite)) {
                Overwrite = true;
            }

            if (Options.Contains(TransferOptions.CreatePath)) {
                var Folder = DestFile.Parse().AsPath().Directory;

                Directory.CreateDirectory(Folder);
            }

            if (Move) {
                File.Move(SourceFile, DestFile, Overwrite);
            } else {
                File.Copy(SourceFile, DestFile, Overwrite);
            }


        }



    }
}
