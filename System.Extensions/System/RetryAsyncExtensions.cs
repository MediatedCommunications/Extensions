using System.Threading;
using System.Threading.Tasks;

namespace System
{
    public static class RetryAsyncExtensions
    {
        public static RetryAsync<T> WithAction<T>(this RetryAsync<T> This, Func<CancellationToken, Task<T>> Value)
        {
            return This with
            {
                Try = Value,
            };
        }

        public static RetryAsync<T> DefaultIs<T>(this RetryAsync<T> This, Func<CancellationToken, Task<T>> Value)
        {
            return This with
            {
                Default = Value,
            };
        }

        public static RetryAsync<T> DefaultIs<T>(this RetryAsync<T> This, Func<CancellationToken, T> Value) {
            return This with {
                Default = x => Task.FromResult(Value(x)),
            };
        }

        public static RetryAsync<T?> DefaultIsNull<T>(this RetryAsync<T> This) {
            
            var OldTry = This.Try;

            async Task<T?> NewTry(CancellationToken Token) {
                var tret = default(T?);

                tret = await OldTry(Token)
                    .DefaultAwait()
                    ;

                return tret;

            }
            
            var ret = new RetryAsync<T?>() {
                DelayAfterRecover = This.DelayAfterRecover,
                DelayBeforeRecover = This.DelayBeforeRecover,
                OnFailure = This.OnFailure,
                Default = x => Task.FromResult(default(T?)),
                Recover = This.Recover,
                MaxAttempts = This.MaxAttempts,
                Token = This.Token,
                Try = NewTry,
            };

            return ret;
        }

        public static RetryAsync<T> DefaultIs<T>(this RetryAsync<T> This, Func<T> Value) {
            return This.DefaultIs(x => Value());
        }

        public static RetryAsync<T> DefaultIs<T>(this RetryAsync<T> This, T Value) {
            return This.DefaultIs(x => Value);
        }

    }


}
