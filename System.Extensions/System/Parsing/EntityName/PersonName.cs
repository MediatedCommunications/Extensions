using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace System {
    public record PersonName : EntityName {
        public string Prefix { get; init; } = string.Empty;

        public string First { get; init; } = string.Empty;
        public ImmutableArray<string> Middle { get; init; } = ImmutableArray<string>.Empty;
        public string Last { get; init; } = string.Empty;

        public string Suffix { get; init; } = string.Empty;

        public override string Full {
            get {
                var V1 = new List<string>() {
                    Prefix,
                    First,
                    Middle,
                    Last,
                }.WhereIsNotBlank().JoinSpace();

                var V2 = new List<string>() {
                    V1,
                    Suffix
                }.WhereIsNotBlank().Join(", ");

                var ret = V2;

                return ret;

            }
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Prefix.Add(Prefix)
                .Data.Add(First)
                .Data.Add(Middle)
                .Data.Add(Last)
                .Postfix.Add(Suffix)
                ;
        }

    }

}
