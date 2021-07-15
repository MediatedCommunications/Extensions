using System.Runtime.CompilerServices;

namespace System.Threading.Tasks {
    public static class AwaitExtensions {
        private const bool __RunOnAnyThread = false;
        private const bool __RunOnThisThread = true;

        //Configure Awaits

        internal static ConfiguredTaskAwaitable ConfigureAwait(Task? This, bool ContinueOnCapturedContext) {
            var TaskToRun = This ?? Task.CompletedTask;

            return TaskToRun.ConfigureAwait(ContinueOnCapturedContext);
        }

        internal static ConfiguredTaskAwaitable<T> ConfigureAwait<T>(Task<T> This, bool ContinueOnCapturedContext) {
            var TaskToRun = This;

            return TaskToRun.ConfigureAwait(ContinueOnCapturedContext);
        }

        internal static ConfiguredValueTaskAwaitable ConfigureAwait(this ValueTask? This, bool ContinueOnCapturedContext) {
            var TaskToRun = This ?? ValueTask.CompletedTask;

            return TaskToRun.ConfigureAwait(ContinueOnCapturedContext);
        }

        internal static ConfiguredValueTaskAwaitable<T> ConfigureAwait<T>(this ValueTask<T> This, bool ContinueOnCapturedContext) {
            var TaskToRun = This;

            return TaskToRun.ConfigureAwait(ContinueOnCapturedContext);
        }

        //DefaultAwait

        public static ConfiguredTaskAwaitable DefaultAwait(this Task? This) {
            return This.ContinueOnAnyThread();
        }

        public static ConfiguredTaskAwaitable<T> DefaultAwait<T>(this Task<T> This) {
            return This.ContinueOnAnyThread();
        }

        public static ConfiguredValueTaskAwaitable DefaultAwait(this ValueTask? This) {
            return This.ContinueOnAnyThread();
        }

        public static ConfiguredValueTaskAwaitable<T> DefaultAwait<T>(this ValueTask<T> This) {
            return This.ContinueOnAnyThread();
        }


        //Continue on Any Thread

        public static ConfiguredTaskAwaitable ContinueOnAnyThread(this Task? This) {
            return ConfigureAwait(This, __RunOnAnyThread);
        }

        public static ConfiguredTaskAwaitable<T> ContinueOnAnyThread<T>(this Task<T> This) {
            return ConfigureAwait(This, __RunOnAnyThread);
        }

        public static ConfiguredValueTaskAwaitable ContinueOnAnyThread(this ValueTask? This) {
            return ConfigureAwait(This, __RunOnAnyThread);
        }

        public static ConfiguredValueTaskAwaitable<T> ContinueOnAnyThread<T>(this ValueTask<T> This) {
            return This.ConfigureAwait(__RunOnAnyThread);
        }

        //Continue on This Thread

        public static ConfiguredTaskAwaitable ContinueOnThisThread(this Task? This) {
            return ConfigureAwait(This, __RunOnThisThread);
        }

        public static ConfiguredTaskAwaitable<T> ContinueOnThisThread<T>(this Task<T> This) {
            return ConfigureAwait(This, __RunOnThisThread);
        }

        public static ConfiguredValueTaskAwaitable ContinueOnThisThread(this ValueTask? This) {
            return ConfigureAwait(This, __RunOnThisThread);
        }

        public static ConfiguredValueTaskAwaitable<T> ContinueOnThisThread<T>(this ValueTask<T> This) {
            return ConfigureAwait(This, __RunOnThisThread);
        }


    }

}
