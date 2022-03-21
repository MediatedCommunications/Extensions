using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Immutable;

namespace System
{
    public record RegexMatchParser : ListParser<Match> {

        public Regex Regex { get; init; } = RegularExpressions.None;

        public override bool TryGetValue(string? Input, [NotNullWhen(true)] out ImmutableList<Match>? Result) {
            var MatchList = new List<Match>();
            if(Input is { } V1) {
                foreach (var item in Regex.Matches(V1).OfType<Match>()) {
                    MatchList.Add(item);
                }
            }
            var tret = MatchList.ToImmutableList();

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
