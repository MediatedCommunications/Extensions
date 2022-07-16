using System.Diagnostics;

namespace System {
    public record NullableValue<T> : DisplayRecord {
        public T? Value { get; init; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Value)
                ;
        }
    }

    public static class NullableValue {

        public static NullableValue<T> Create<T>(T Value) {
            var ret = new NullableValue<T>()
            {
                Value = Value
            };

            return ret;

        }
    }

}