using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Collections.Generic {

    public static class AwaitExtensions {
        private const bool __ContinueOnAnyThread = false;
        private const bool __ContinueOnThisThread = true;

        public static ConfiguredCancelableAsyncEnumerable<T> DefaultAwait<T>(this IAsyncEnumerable<T> This) {
            return This.ContinueOnAnyThread();
        }

        public static ConfiguredCancelableAsyncEnumerable<T> ContinueOnAnyThread<T>(this IAsyncEnumerable<T> This) {
            return This.ConfigureAwait(__ContinueOnAnyThread);
        }

        public static ConfiguredCancelableAsyncEnumerable<T> ContinueOnThisThread<T>(this IAsyncEnumerable<T> This) {
            return This.ConfigureAwait(__ContinueOnThisThread);
        }


    }

}
