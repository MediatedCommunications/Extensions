using System.Diagnostics;

namespace System {
    public record FormatValue<T> : DisplayRecord {
        public T Value { get; init; }
        public FormatValue(T Value) {
            this.Value = Value;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add($@"{Value}")
                ;
        }
    }

}
