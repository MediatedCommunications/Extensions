using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    public record RetrySync<T> : RetryBase
    {
        public Func<CancellationToken, T> Try { get; init; } = x => throw new MissingMethodException();
        public Func<CancellationToken, T> Default { get; init; } = x => throw new MissingMethodException();

        public Action<Exception, CancellationToken> Recover { get; init; } = (x, y) => { };


        public T Invoke(CancellationToken Token = default)
        {
            var LinkedToken = CancellationTokenSource.CreateLinkedTokenSource(Token, this.Token);

            var ret = default(T);
            var Attempts = 0;
            var Success = false;

            var FailureException = default(ExceptionDispatchInfo);

            while (!Success && Attempts < RetryAttempts && LinkedToken.Token.ShouldContinue())
            {
                try
                {
                    Attempts += 1;

                    ret = Try(LinkedToken.Token);
                    FailureException = default;
                    Success = true;
                } catch (Exception ex)
                {
                    FailureException = ExceptionDispatchInfo.Capture(ex);

                    if (Attempts != RetryAttempts)
                    {
                        SafeDelay.Delay(DelayBeforeRecover, LinkedToken.Token)
                            ;

                        Recover(ex, LinkedToken.Token)
                            ;

                        SafeDelay.Delay(DelayAfterRecover, LinkedToken.Token)
                            ;
                    }

                }

            }

            if (FailureException is { })
            {
                if (OnFailure == RetryFailureResult.ThrowException)
                {
                    FailureException.Throw();
                }
            }

            if (ret is null)
            {
                ret = Default(Token)
                    ;
            }

            return ret;

        }



    }

}
