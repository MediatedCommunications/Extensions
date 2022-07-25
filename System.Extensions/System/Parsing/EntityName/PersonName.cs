using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace System {

    public record PersonName : EntityName {
        public string Prefix { get; init; } = Strings.Empty;

        public string First { get; init; } = Strings.Empty;
        public ImmutableArray<string> Middle { get; init; } = ImmutableArray<string>.Empty;
        public string Last { get; init; } = Strings.Empty;

        public string Suffix { get; init; } = Strings.Empty;

        public PersonName() { }

        public PersonName(string? Last, string? First, params string?[] Middle) : this(Last, First, Middle.AsEnumerable()) {

        }

        public PersonName(string? Last, string? First, IEnumerable<string?>? Middle) {
            this.First = First.Coalesce();
            this.Middle = Middle.WhereIsNotBlank().ToImmutableArray();
            this.Last = Last.Coalesce();
        }

        public override string Full {
            get {
                var ret = this.Format().AsString(PersonNameFormats.FirstMiddleLast);

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
