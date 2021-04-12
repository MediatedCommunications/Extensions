using System.Diagnostics.Contracts;

namespace System.Diagnostics {

    public class Debugger2 {

        public const string DebuggerDisplay = "{GetDebuggerDisplay(),nq}";

        public static bool IsAttached {
            get {
                return Debugger.IsAttached;
            }
        }

        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/fe36c6d9-fc3d-4a81-9156-da19d6117c6c/bug-static-checking-inverts-custom-parameter-validation-logic?forum=codecontracts
        //When I put the DebuggerHidden on it, it causes the break to occur at the callign function, not at this function.
        [Conditional(Build.DEBUG)]
        [DebuggerHidden, Pure]
        public static void BreakIfAttached() {
            if (Debugger.IsAttached) {
                Debugger.Break();
            }
        }

    }
}
