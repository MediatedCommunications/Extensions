using System.Threading;

namespace System
{
    public static class RetrySyncExtensions
    {
        public static RetrySync<T> WithAction<T>(this RetrySync<T> This, Func<CancellationToken, T> Value)
        {
            return This with
            {
                Try = Value,
            };
        }

        public static RetrySync<T> WithDefault<T>(this RetrySync<T> This, Func<CancellationToken, T> Value)
        {
            return This with
            {
                Default = Value,
            };
        }

    }


}
