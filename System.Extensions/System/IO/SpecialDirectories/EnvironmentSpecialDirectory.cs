using System.Collections.Generic;
using System.Linq;

namespace System.IO {
    internal class EnvironmentSpecialDirectory : SpecialDirectory {
        protected System.Environment.SpecialFolder Folder { get; }
        protected string[] SubPath { get; }

        public EnvironmentSpecialDirectory(System.Environment.SpecialFolder Folder, params string[] SubPath) {
            this.Folder = Folder;
            this.SubPath = SubPath.ToArray();
        }

        public override string GetPath() {
            var Paths = new List<string>() {
                System.Environment.GetFolderPath(Folder),
                SubPath
            }.ToArray();

            var ret = System.IO.Path.Combine(Paths);

            return ret;
        }

    }



}
