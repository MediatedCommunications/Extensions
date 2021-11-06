using System.Diagnostics;

namespace System.Events.Async {
    public class MethodEventArgs {

        public static MethodEventArgs<TData> Create<TData>(TData Data) {
            return new MethodEventArgs<TData>(Data);
        }

    }

    public class MethodEventArgs<TData>
        : DisplayClass
        , IHandled
        , IData<TData> {
        public bool Handled { get; set; }
        public TData Data { get; }

        public MethodEventArgs(TData Data) {
            this.Data = Data;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Data)
                .Postfix.AddFlag(Handled)
                ;
        }

    }

}
