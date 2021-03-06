using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    public record RetrySync<T> : RetryBase
    {
        public Func<int, CancellationToken, T> Try { get; init; } = (x,y) => throw new MissingMethodException();
        public Func<CancellationToken, T> Default { get; init; } = x => throw new MissingMethodException();

        public Action<Exception, CancellationToken> Recover { get; init; } = (x, y) => { };

        public RetryResult<T> TryInvoke(CancellationToken Token = default) {
            return TryInvoke(false, Token);
        }

        public T Invoke(CancellationToken Token = default) { 
            var tret = TryInvoke(true, Token);

            var ret = tret.Result;

            return ret;
        }

        private RetryResult<T> TryInvoke(bool CanThrowException, CancellationToken Token) {
            var LinkedToken = CancellationTokenSources.Create(Token, this.Token);

            var Result_Success = false;
            var Result_Exception = default(Exception?);
            var Result_Value = default(T)!;

            var Attempts = 0;

            var FailureException = default(ExceptionDispatchInfo);

            while (!Result_Success && Attempts < MaxAttempts && LinkedToken.Token.ShouldContinue())
            {
                try
                {
                    var tret = Try(Attempts, LinkedToken.Token);


                    Result_Success = true;
                    Result_Value = tret;
                    Result_Exception = default;


                    FailureException = default;
                } catch (Exception ex)
                {
                    FailureException = ExceptionDispatchInfo.Capture(ex);

                    if (Attempts != MaxAttempts)
                    {
                        var Delay1 = DelayBeforeRecover(Attempts);
                        SafeDelay.Delay(Delay1, LinkedToken.Token)
                            ;

                        Recover(ex, LinkedToken.Token)
                            ;

                        var Delay2 = DelayAfterRecover(Attempts);
                        SafeDelay.Delay(Delay2, LinkedToken.Token)
                            ;
                    }

                }
                Attempts += 1;
            }

            if (FailureException is { }) {
                Result_Success = false;
                Result_Exception = FailureException.SourceException;

                if (CanThrowException && OnFailure == RetryFailureResult.ThrowException) {
                    FailureException.Throw();
                }
            }

            if (!Result_Success) {
                Result_Value = Default(Token)
                    ;
            }

            var ret = new RetryResult<T>(Result_Value) {
                Exception = Result_Exception,
                Result = Result_Value,
                Success = Result_Success,
            };

            return ret;

        }



    }

}
