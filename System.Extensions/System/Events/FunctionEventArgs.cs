using System.Diagnostics;

namespace System.Events.Async {
    public static class FunctionEventArgs {
        public static FunctionEventArgs<TData, TResult> Create<TData, TResult>(TData Data, TResult? DefaultResult) {
            return new FunctionEventArgs<TData, TResult>(Data, DefaultResult);
        }
    }

    public class FunctionEventArgs<TData, TResult>
        : MethodEventArgs<TData>
        , IResult<TResult>
        {
        public FunctionEventArgs(TData Data, TResult? DefaultResult) : base(Data) {
            this.Result = DefaultResult;
        }

        public TResult? Result { get; set; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Postfix.Add(Result)
                ;
        }


    }

}
