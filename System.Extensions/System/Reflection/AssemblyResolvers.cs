using System.Runtime.Loader;

namespace System.Reflection {
    public static class AssemblyResolvers {
        public static AssemblyResolver EntryFolder { get; }
        public static AssemblyResolver None { get; }

        static AssemblyResolvers() {
            None = new NullAssemblyResolver();
            EntryFolder = FromFilePath(Entry.Assembly.FolderPath);
        }

        public static AssemblyResolver FromAssemblyPath(Assembly? Asm, AssemblyLoadContext? Context = default) {
            var Location = Asm?.Location;
            var Path = IO.Path.GetDirectoryName(Location) ?? Strings.Empty;

            return FromFilePath(Path, Context);
        }

        public static AssemblyResolver FromFilePath(string Path, AssemblyLoadContext? Context = default) {
            return new PathAssemblyResolver(Path, Context);
        }

    }


}
