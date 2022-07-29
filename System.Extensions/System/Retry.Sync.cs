using System.Threading;
using System.Threading.Tasks;

namespace System {

    public static partial class Retry
    {
        public static RetrySync<bool> WithAction(Action Value) {
            bool SubAction(int Attempt, CancellationToken Token) {
                Attempt.Ignore();
                Token.Ignore();
                Value();

                return true;
            }

            var ret = new RetrySync<bool>() {
                Try = SubAction,
            }.DefaultIs(false);

            return ret;
        }

        public static RetrySync<bool> WithAction(Action<CancellationToken> Value)
        {
            bool SubAction(int Attempt, CancellationToken Token)
            {
                Attempt.Ignore();
                Value(Token);

                return true;
            }

            

            var ret = new RetrySync<bool>()
            {
                Try = SubAction,
            }.DefaultIs(false);;

            return ret;

        }

        public static RetrySync<bool> WithAction(Action<int, CancellationToken> Value) {
            bool SubAction(int Attempt, CancellationToken Token) {
                Value(Attempt, Token);

                return true;
            }



            var ret = new RetrySync<bool>()
            {
                Try = SubAction,
            }.DefaultIs(false); ;

            return ret;

        }


        public static RetrySync<T> WithAction<T>(Func<T> Value) {
            T SubAction(int Attempt, CancellationToken Token) {
                Attempt.Ignore();
                Token.Ignore();

                var tret = Value();

                return tret;
            }

            var ret = new RetrySync<T>() {
                Try = SubAction,
            };

            return ret;
        }

        public static RetrySync<T> WithAction<T>(Func<CancellationToken, T> Value) {
            T SubAction(int Attempt, CancellationToken Token) {
                Attempt.Ignore();

                var tret = Value(Token);

                return tret;
            }

            var ret = new RetrySync<T>()
            {
                Try = SubAction,
            };

            return ret;
        }

        public static RetrySync<T> WithAction<T>(Func<int, CancellationToken, T> Value)
        {
            var ret = new RetrySync<T>()
            {
                Try = Value
            };

            return ret;
        }

    }


}
