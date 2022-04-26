using System.Diagnostics;

namespace System {
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

}
