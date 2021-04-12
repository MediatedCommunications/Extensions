using System.Diagnostics;

namespace System {
    public record ParseValue : DisplayRecord {
        public ParseValue(string? Value) {
            this.Value = Value;
        }

        public string? Value { get; init; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Value)
                ;
        }

    }


}
