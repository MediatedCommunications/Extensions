using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace System {
    public record RegexValuesParser : ListParser<string> {
        public RegexValuesParser(Regex Regex, string? Value) : base(Value) {
            this.Regex = Regex;
        }

        public Regex Regex { get; init; }

        public override bool TryGetValue([NotNullWhen(true)] out LinkedList<string>? Result) {
            var tret = new LinkedList<string>();
            if (Input is { } V1) {
                foreach (var item in Regex.Matches(V1).OfType<Match>()) {
                    tret.Add(item.Value);
                }
            }

            bool ret;
            (ret, Result) = tret.Count > 0
                ? (true, tret)
                : (false, default)
                ;

            return ret;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Postfix.Add(Regex)
                ;
        }

    }


}
