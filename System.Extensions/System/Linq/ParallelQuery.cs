using System.Diagnostics;

namespace System.Linq {
    public static class ParallelQueryExtensions {

        public static ParallelQuery<T> SingleThread<T>(this ParallelQuery<T> This, bool SingleThread = true) {
            var ret = This;
            if (SingleThread) {
                ret = This.WithDegreeOfParallelism(1);
            }

            return ret;
        }

        public static ParallelQuery<T> SingleThreadInDebug<T>(this ParallelQuery<T> This) {
            return This.SingleThread(Debugger2.WasAttachedAtStartup);
            
        }
    }
}
