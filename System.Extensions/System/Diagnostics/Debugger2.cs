using System.Diagnostics.Contracts;
using System.Threading;

namespace System.Diagnostics {

    public class Debugger2 {

        public const string GetDebuggerDisplay = "{GetDebuggerDisplay(),nq}";

        static Debugger2() {
            WasAttachedAtStartup = IsAttached;
        }

        public static bool IsAttached {
            get {
                return Debugger.IsAttached;
            }
        }

        public static bool WasAttachedAtStartup { get; }


        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/fe36c6d9-fc3d-4a81-9156-da19d6117c6c/bug-static-checking-inverts-custom-parameter-validation-logic?forum=codecontracts
        //When I put the DebuggerHidden on it, it causes the break to occur at the calling function, not at this function.
        [Conditional(Build.DEBUG)]
        [DebuggerHidden, Pure]
        public static void BreakIfAttached() {
            if (IsAttached) {
                Break();
            }
        }

        [Conditional(Build.DEBUG)]
        [DebuggerHidden, Pure]
        public static void Break() {
            Debugger.Break();
        }

    }
}
