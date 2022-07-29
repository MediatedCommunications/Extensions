using System.Threading;

namespace System
{
    public static class RetrySyncExtensions
    {
        public static RetrySync<T> WithAction<T>(this RetrySync<T> This, Func<CancellationToken, T> Value)
        {
            T NewTry(int Attempt, CancellationToken Token) {
                return Value(Token);
            }

            return This with
            {
                Try = NewTry,
            };
        }

        public static RetrySync<T> WithAction<T>(this RetrySync<T> This, Func<int, CancellationToken, T> Value) {
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

        public static RetrySync<TResult> DefaultIs<TResult>(this RetrySync<TResult> This, TResult Default)
        {

            return DefaultIs(This, x => Default);

        }

    }


}
