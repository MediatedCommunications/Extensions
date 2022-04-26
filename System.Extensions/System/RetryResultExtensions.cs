using System.Diagnostics.CodeAnalysis;

namespace System {
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

}
