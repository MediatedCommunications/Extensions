namespace System.IO {
    internal class EnvironmentSpecialDirectory : SpecialDirectory {
        protected System.Environment.SpecialFolder Folder { get; }

        public EnvironmentSpecialDirectory(System.Environment.SpecialFolder Folder) {
            this.Folder = Folder;
        }

        public override string GetPath() {
            var ret = System.Environment.GetFolderPath(Folder);

            return ret;
        }

    }



}
