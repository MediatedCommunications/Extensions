using System.Threading;
using System.Threading.Tasks;

namespace System {

    public static partial class Retry
    {
        public static RetryAsync<bool> WithActionAsync(Func<Task> Value) {
            async Task<bool> SubAction(int Attempt, CancellationToken Token) {
                Attempt.Ignore();
                Token.Ignore();

                await Value()
                    .DefaultAwait()
                    ;

                return true;
            }



            var ret = new RetryAsync<bool>() {
                Try = SubAction,
            }.DefaultIs(false);

            return ret;
        }


        public static RetryAsync<bool> WithActionAsync(Func<CancellationToken, Task> Value) { 
            async Task<bool> SubAction(int Attempt, CancellationToken Token)
            {
                Attempt.Ignore();
                await Value(Token)
                    .DefaultAwait()
                    ;

                return true;
            }



            var ret = new RetryAsync<bool>()
            {
                Try = SubAction,
            }.DefaultIs(false);

            return ret;

        }

        public static RetryAsync<bool> WithActionAsync(Func<int, CancellationToken, Task> Value) {
            async Task<bool> SubAction(int Attempt, CancellationToken Token) {
                await Value(Attempt, Token)
                    .DefaultAwait()
                    ;

                return true;
            }



            var ret = new RetryAsync<bool>()
            {
                Try = SubAction,
            }.DefaultIs(false);

            return ret;

        }


        public static RetryAsync<T> WithActionAsync<T>(Func<Task<T>> Value) {
            async Task<T> SubAction(int Attempt, CancellationToken Token) {
                Attempt.Ignore();
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

        public static RetryAsync<T> WithActionAsync<T>(Func<CancellationToken, Task<T>> Value) {
            async Task<T> SubAction(int Attempt, CancellationToken Token) {
                Token.Ignore();

                var ret = await Value(Token)
                    .DefaultAwait()
                    ;

                return ret;
            }

            var ret = new RetryAsync<T>()
            {
                Try = SubAction,
            };

            return ret;

        }

        public static RetryAsync<T> WithActionAsync<T>(Func<int, CancellationToken, Task<T>> Value)
        {
            var ret = new RetryAsync<T>()
            {
                Try = Value
            };

            return ret;
        }

    }


}
