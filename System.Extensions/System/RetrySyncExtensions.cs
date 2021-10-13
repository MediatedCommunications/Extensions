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

        public static RetrySync<T> DefaultIs<T>(this RetrySync<T> This, Func<CancellationToken, T> Value)
        {
            return This with
            {
                Default = Value,
                OnFailure = RetryFailureResult.ReturnDefault,
            };
        }

        public static RetrySync<TResult> DefaultIs<TResult>(this RetrySync<TResult> This, TResult Default = default)
            where TResult : struct {

            return DefaultIs(This, x => Default);

        }

    }


}
