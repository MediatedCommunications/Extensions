namespace System {
    internal class MyTempFolder : SpecialFolder {
        public override string GetPath() {
            var ret = System.IO.Path.GetTempPath();

            return ret;
        }
    }



}
