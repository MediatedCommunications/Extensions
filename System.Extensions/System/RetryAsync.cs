using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System {

    public enum RetryFailureResult {
        ReturnDefault,
        ThrowException,
    }

    public record RetryAsync<T> {
        public Func<CancellationToken, Task<T>> Try { get; init; } = x => Task.FromResult(default(T));
        public Func<Exception, CancellationToken, Task> Recover { get; init; } = (x,y) => Task.CompletedTask;
        public TimeSpan DelayBeforeRecover { get; init; } = TimeSpan.Zero;
        public TimeSpan DelayAfterRecover { get; init; } = TimeSpan.FromSeconds(1);

        public long RetryAttempts { get; init; } = 3;

        public RetryFailureResult OnFailure { get; init; } = RetryFailureResult.ReturnDefault;
        public CancellationToken Token { get; init; } = default;

        public async Task<T> InvokeAsync(CancellationToken Token = default) {
            var LinkedToken = CancellationTokenSource.CreateLinkedTokenSource(Token, this.Token);

            var ret = default(T);
            var Attempts = 0;
            var Success = false;

            var FailureException = default(ExceptionDispatchInfo);

            while(!Success && Attempts < RetryAttempts && LinkedToken.Token.ShouldContinue()) {
                try {
                    Attempts += 1;

                    ret = await Try(LinkedToken.Token)
                        .DefaultAwait()
                        ;
                    Success = true;
                } catch (Exception ex) {
                    if(Attempts != RetryAttempts) {

                        await SafeDelay.DelayAsync(DelayBeforeRecover, LinkedToken.Token)
                            .DefaultAwait()
                            ;

                        await Recover(ex, LinkedToken.Token)
                            .DefaultAwait()
                            ;

                        await SafeDelay.DelayAsync(DelayAfterRecover, LinkedToken.Token)
                            .DefaultAwait()
                            ;
                    } else {
                        FailureException = ExceptionDispatchInfo.Capture(ex);
                    }

                }

            }

            if(Success == false) {
                if(OnFailure == RetryFailureResult.ThrowException) {
                    FailureException.Throw();
                }
            }

            return ret;

        }
        
        

    }

}
