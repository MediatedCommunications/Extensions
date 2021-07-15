namespace System.Events.Async {
    public static class AsyncDelegateExtensions {
        public static AsyncFunction<TSender, TData, TResult> AsAsyncHandler<TSender, TData, TResult>(this AsyncDelegate<TSender, FunctionEventArgs<TData, TResult>>? This, TResult? DefaultResult = default, bool ThrowOnError = default) {
            var ret = new AsyncFunction<TSender, TData, TResult>() {
                DefaultResult = DefaultResult,
                ThrowOnError = ThrowOnError,
            };
            ret.Handler += This;

            return ret;
        }

        public static AsyncMethod<TSender, TData> AsAsyncHandler<TSender, TData>(this AsyncDelegate<TSender, MethodEventArgs<TData>>? This, bool ThrowOnError = default) {
            var ret = new AsyncMethod<TSender, TData>() {
                ThrowOnError = ThrowOnError,
            };
            ret.Handler += This;

            return ret;
        }

    }
}
