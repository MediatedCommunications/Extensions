using System.Threading;
using System.Threading.Tasks;

namespace System
{
    public static class Retry
    {
        public static RetryAsync<bool> WithActionAsync(Func<CancellationToken, Task> Value) { 
            async Task<bool> SubAction(CancellationToken Token)
            {
                await Value(Token)
                    .DefaultAwait()
                    ;

                return true;
            }

            static Task<bool> Default(CancellationToken Token)
            {

                return Task.FromResult(false);
            }

            var ret = new RetryAsync<bool>()
            {
                Try = SubAction,
                Default = Default,
            };

            return ret;

        }

        public static RetryAsync<T> WithActionAsync<T>(Func<CancellationToken, Task<T>> Value)
        {
            var ret = new RetryAsync<T>()
            {
                Try = Value
            };

            return ret;
        }

        public static RetrySync<bool> WithAction(Action<CancellationToken> Value)
        {
            bool SubAction(CancellationToken Token)
            {
                Value(Token);

                return true;
            }

            static bool Default(CancellationToken Token)
            {

                return false;
            }

            var ret = new RetrySync<bool>()
            {
                Try = SubAction,
                Default = Default,
            };

            return ret;

        }

        public static RetrySync<T> WithAction<T>(Func<CancellationToken, T> Value)
        {
            var ret = new RetrySync<T>()
            {
                Try = Value
            };

            return ret;
        }

        public static T OnFailure<T>(this T This, RetryFailureResult Value) where T : RetryBase
        {
            return This with
            {
                OnFailure = Value
            };
        }
    }


}
