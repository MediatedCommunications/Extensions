namespace System {
    internal class EnvironmentSpecialFolder : SpecialFolder {
        protected System.Environment.SpecialFolder Folder { get; }

        public EnvironmentSpecialFolder(System.Environment.SpecialFolder Folder) {
            this.Folder = Folder;
        }

        public override string GetPath() {
            var ret = System.Environment.GetFolderPath(Folder);

            return ret;
        }

    }



}
