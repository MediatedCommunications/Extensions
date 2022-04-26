using System.Diagnostics;
using System.Runtime.Loader;

namespace System.Reflection {
    public class PathAssemblyResolver : AssemblyResolver {
        protected string Path { get; }


        public PathAssemblyResolver(string Path, AssemblyLoadContext? Context) : base(Context) {
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

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Path)
                ;
        }

    }

}
