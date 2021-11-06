namespace System.IO {
    internal class MyTempDirectory : SpecialDirectory {
        public override string GetPath() {
            var ret = System.IO.Path.GetTempPath();

            return ret;
        }
    }



}
