using System.Diagnostics;
using System.Runtime.Loader;

namespace System.Reflection {
    public class AssemblyResolver : DisplayClass  {

        protected AssemblyLoadContext Context { get; }

        public AssemblyResolver(AssemblyLoadContext? Context = default) {
            this.Context = Context ?? AssemblyLoadContext.Default;
        }

        public void Enable() {
            Enabled = true;
        }

        public void Disable() {
            Enabled = false;
        }

        private bool __Enabled;
        public bool Enabled {
            get {
                return __Enabled;
            }
            set {
                if (value != __Enabled) {
                    if (value) {
                        Context.Resolving += Context_Resolving;
                    } else {
                        Context.Resolving -= Context_Resolving;
                    }
                    __Enabled = value;
                }
            }
        }

        protected virtual Assembly? Context_Resolving(AssemblyLoadContext Context, AssemblyName Name) {
            return default;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Status.IsEnabled(Enabled)
                ;
        }

    }

    public sealed class NullAssemblyResolver : AssemblyResolver {

    }
}
