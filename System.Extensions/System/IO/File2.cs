namespace System.IO {
    public static class File2 {

        public static bool IsTemporaryName(string FileName) {

            var ext = FileName.Parse().AsPath().DotExtension.AsText();

            var ret = false
                || ext.EndsWith(".crdownload")
                || ext.EndsWith(".tmp")
                || ext.EndsWith(".temp")
                || ext.StartsWith("~")
                || ext.StartsWith("._")
                ;

            return ret;
        }

        public static bool IsSystemGeneratedName(string FileName) {
            var FN = FileName.Parse().AsPath().FileName.AsText();

            var ret = false
                || FN.Equals("desktop.ini")
                || FN.Equals("thumbs.db")
                || FN.Equals(".ds_store")
                ;

            return ret;
        }

    }
}
