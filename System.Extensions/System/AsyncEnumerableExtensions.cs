namespace System {
    public static class AsyncEnumerableExtensions {
        public static GracefulExceptionAsyncEnumerable<T> WithGracefulExceptions<T>(this IAsyncEnumerable<T> This, Action<Exception>? OnError = default) {
            
            Task SubAction(Exception ex) {
                OnError?.Invoke(ex);
                return Task.CompletedTask;
            }
            
            return WithGracefulExceptions(This, OnError is { } ? SubAction : null);
        }

        public static GracefulExceptionAsyncEnumerable<T> WithGracefulExceptions<T>(this IAsyncEnumerable<T> This, Func<Exception, Task>? OnError = default) {
            var ret = new GracefulExceptionAsyncEnumerable<T> { 
                Source = This, 
                OnError = OnError 
            };

            return ret;
        }
    }
}
