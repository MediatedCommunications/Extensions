using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System
{

    public record RetryResult<T> : DisplayRecord {
        public bool Success { get; init; }
        public Exception? Exception { get; init; }
        public T Result {  get; init; }

        public RetryResult(T Result) {
            this.Result = Result;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Status.IsSuccess(Success);
        }
    }

    public static class RetryResultExtensions { 
        public static bool IsSuccess<T>(this RetryResult<T> This, out T Result) {
            Result = This.Result;
            return This.Success;
        }

        public static bool IsError<T>(this RetryResult<T> This, out T ResultOrDefault) {
            return This.IsError(out ResultOrDefault);
        }

        public static bool IsError<T>(this RetryResult<T> This, out T ResultOrDefault, [NotNullWhen(true)] out Exception? Error) {
            var ret = !This.Success;

            ResultOrDefault = This.Result;

            Error = default;

            if (ret) {
                Error = This.Exception ?? new Exception();
            }

            
            return ret;
        }

    }


    public record RetryAsync<T> : RetryBase {
        public Func<CancellationToken, Task<T>> Try { get; init; } = x => throw new MissingMethodException();
        public Func<CancellationToken, Task<T>> Default { get; init; } = x => throw new MissingMethodException();

        public Func<Exception, CancellationToken, Task> Recover { get; init; } = (x,y) => Task.CompletedTask;

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
            var LinkedToken = CancellationTokenSource.CreateLinkedTokenSource(Token, this.Token);

            var Result_Success = false;
            var Result_Exception = default(Exception?);
            var Result_Value = default(T)!;

            var Attempts = 0;

            var FailureException = default(ExceptionDispatchInfo);

            while(!Result_Success && Attempts < RetryAttempts && LinkedToken.Token.ShouldContinue()) {
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

                    if (Attempts != RetryAttempts) {

                        await SafeDelay.DelayAsync(DelayBeforeRecover, LinkedToken.Token)
                            .DefaultAwait()
                            ;

                        await Recover(ex, LinkedToken.Token)
                            .DefaultAwait()
                            ;

                        await SafeDelay.DelayAsync(DelayAfterRecover, LinkedToken.Token)
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
