using System.Threading;
using System.Threading.Tasks;

namespace System {
    public static class RetryExtensions {
        public static T OnFailure<T>(this T This, RetryFailureResult Value) where T : RetryBase {
            return This with {
                OnFailure = Value
            };
        }

    }

    public static class RetryExtensionsAsnyc {

        public static RetryAsync<TResult> DefaultIs<TResult>(this RetryAsync<TResult> This, TResult Default = default)
            where TResult : struct {

            return DefaultIs(This, x => Task.FromResult(Default));
        }

        public static RetryAsync<TResult> DefaultIs<TResult>(this RetryAsync<TResult> This, Func<CancellationToken, Task<TResult>> Default) {

            return This with {
                Default = Default,
                OnFailure = RetryFailureResult.ReturnDefault,
            };

        }
    }


    public static class Retry
    {
        public static RetryAsync<bool> WithActionAsync(Func<Task> Value) {
            async Task<bool> SubAction(CancellationToken Token) {
                Token.Ignore();

                await Value()
                    .DefaultAwait()
                    ;

                return true;
            }



            var ret = new RetryAsync<bool>() {
                Try = SubAction,
            }.DefaultIs();

            return ret;
        }


        public static RetryAsync<bool> WithActionAsync(Func<CancellationToken, Task> Value) { 
            async Task<bool> SubAction(CancellationToken Token)
            {
                await Value(Token)
                    .DefaultAwait()
                    ;

                return true;
            }



            var ret = new RetryAsync<bool>()
            {
                Try = SubAction,
            }.DefaultIs();

            return ret;

        }

        public static RetryAsync<T> WithActionAsync<T>(Func<Task<T>> Value) {
            async Task<T> SubAction(CancellationToken Token) {
                Token.Ignore();

                var ret = await Value()
                    .DefaultAwait()
                    ;

                return ret;
            }

            var ret = new RetryAsync<T>() {
                Try = SubAction,
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

            

            var ret = new RetrySync<bool>()
            {
                Try = SubAction,
            }.DefaultIs();

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

    }


}
