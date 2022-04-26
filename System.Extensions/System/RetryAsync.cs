﻿using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System {


    public record RetryAsync<T> : RetryBase {
        public Func<CancellationToken, Task<T>> Try { get; init; } = x => throw new MissingMethodException();
        public Func<CancellationToken, Task<T>> Default { get; init; } = x => throw new MissingMethodException();

        public Func<Exception, int, CancellationToken, Task> Recover { get; init; } = (x, y, z) => Task.CompletedTask;

        public async Task<T> InvokeAsync(CancellationToken Token = default) {
            var tret = await TryInvokeAsync(true, Token)
                .DefaultAwait()
                ;

            var ret = tret.Result;

            return ret;
        }

        public async Task<RetryResult<T>> TryInvokeAsync(CancellationToken Token = default) {
            var ret = await TryInvokeAsync(false, Token)
                .DefaultAwait()
                ;

            return ret;
        }
        
        private async Task<RetryResult<T>> TryInvokeAsync(bool CanThrowException, CancellationToken Token) {
            var LinkedToken = CancellationTokenSources.Create(Token, this.Token);

            var Result_Success = false;
            var Result_Exception = default(Exception?);
            var Result_Value = default(T)!;

            var Attempts = 0;

            var FailureException = default(ExceptionDispatchInfo);

            while(!Result_Success && Attempts < MaxAttempts && LinkedToken.Token.ShouldContinue()) {
                try {
                    Attempts += 1;

                    var tret = await Try(LinkedToken.Token)
                        .DefaultAwait()
                        ;


                    Result_Success = true;
                    Result_Value = tret;
                    Result_Exception = default;
                    

                    FailureException = default;

                } catch (Exception ex) {
                    FailureException = ExceptionDispatchInfo.Capture(ex);

                    if (Attempts != MaxAttempts) {

                        var Delay1 = DelayBeforeRecover(Attempts);
                        await SafeDelay.DelayAsync(Delay1, LinkedToken.Token)
                            .DefaultAwait()
                            ;

                        await Recover(ex, Attempts, LinkedToken.Token)
                            .DefaultAwait()
                            ;

                        var Delay2 = DelayAfterRecover(Attempts);
                        await SafeDelay.DelayAsync(Delay2, LinkedToken.Token)
                            .DefaultAwait()
                            ;
                    }

                }

            }


            if(FailureException is { }) {
                Result_Success = false;
                Result_Exception = FailureException.SourceException;

                if(CanThrowException && OnFailure == RetryFailureResult.ThrowException) {
                    FailureException.Throw();
                }
            }

            if (!Result_Success) {
                Result_Value = await Default(Token)
                    .DefaultAwait()
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
