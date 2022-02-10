using System.Runtime.Loader;

namespace System.Reflection {
    public class PathAssemblyResolver : AssemblyResolver {
        protected string Path { get; private set; }

        private static string GetPath(Assembly? Asm) {
            var Location = Asm?.Location;
            var ret = IO.Path.GetDirectoryName(Location) ?? Strings.Empty;

            return ret;
        }

        public PathAssemblyResolver(Assembly? Asm, AssemblyLoadContext? Context = default) : this(GetPath(Asm), Context) { }

        public PathAssemblyResolver(string Path, AssemblyLoadContext? Context = default) : base(Context) {
            this.Path = Path;
        }

        protected override Assembly? Context_Resolving(AssemblyLoadContext Context, AssemblyName Name) {
            var ret = default(Assembly?);

            var PotentialPath = IO.Path.Combine(Path, $@"{Name.Name}.dll");

            if (IO.File.Exists(PotentialPath)) {
                ret = Context.LoadFromAssemblyPath(PotentialPath);
            }

            return ret;
        }

    }

}
