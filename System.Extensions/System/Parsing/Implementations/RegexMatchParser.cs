using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Linq;

namespace System
{
    public record RegexMatchParser : ListParser<Match> {
        public RegexMatchParser(Regex Regex, string? Value) : base(Value) {
            this.Regex = Regex;
        }

        public Regex Regex { get; init; }

        public override bool TryGetValue([NotNullWhen(true)] out LinkedList<Match>? Result) {
            var tret = new LinkedList<Match>();
            if(Input is { } V1) {
                foreach (var item in Regex.Matches(V1).OfType<Match>()) {
                    tret.Add(item);
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
