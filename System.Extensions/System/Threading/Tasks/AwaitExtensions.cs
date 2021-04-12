using System.Runtime.CompilerServices;

namespace System.Threading.Tasks {
    public static class AwaitExtensions {
        private const bool __RunOnAnyThread = false;
        private const bool __RunOnThisThread = true;
    
        public static ConfiguredTaskAwaitable DefaultAwait(this Task This) {
            return This.ContinueOnAnyThread();
        }

        public static ConfiguredTaskAwaitable<T> DefaultAwait<T>(this Task<T> This) {
            return This.ContinueOnAnyThread();
        }

        public static ConfiguredValueTaskAwaitable DefaultAwait(this ValueTask This) {
            return This.ContinueOnAnyThread();
        }

        public static ConfiguredValueTaskAwaitable<T> DefaultAwait<T>(this ValueTask<T> This) {
            return This.ContinueOnAnyThread();
        }

        public static ConfiguredTaskAwaitable ContinueOnAnyThread(this Task This) {
            return This.ConfigureAwait(__RunOnAnyThread);
        }

        public static ConfiguredTaskAwaitable<T> ContinueOnAnyThread<T>(this Task<T> This) {
            return This.ConfigureAwait(__RunOnAnyThread);
        }

        public static ConfiguredValueTaskAwaitable ContinueOnAnyThread(this ValueTask This) {
            return This.ConfigureAwait(__RunOnAnyThread);
        }

        public static ConfiguredValueTaskAwaitable<T> ContinueOnAnyThread<T>(this ValueTask<T> This) {
            return This.ConfigureAwait(__RunOnAnyThread);
        }

        public static ConfiguredTaskAwaitable ContinueOnThisThread(this Task This) {
            return This.ConfigureAwait(__RunOnThisThread);
        }

        public static ConfiguredTaskAwaitable<T> ContinueOnThisThread<T>(this Task<T> This) {
            return This.ConfigureAwait(__RunOnThisThread);
        }

        public static ConfiguredValueTaskAwaitable ContinueOnThisThread(this ValueTask This) {
            return This.ConfigureAwait(__RunOnThisThread);
        }

        public static ConfiguredValueTaskAwaitable<T> ContinueOnThisThread<T>(this ValueTask<T> This) {
            return This.ConfigureAwait(__RunOnThisThread);
        }

    }

}
