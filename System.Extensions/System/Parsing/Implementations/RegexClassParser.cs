using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace System
{
    public abstract record RegexClassParser<TResult> : ListParser<TResult> {
        public Regex Regex { get; init; } = RegularExpressions.None;

        protected abstract bool TryGetValue(Match Input, [NotNullWhen(true)] out TResult? result);

        public override bool TryGetValue(string? Input, [NotNullWhen(true)] out ImmutableList<TResult>? Result) {
            var ret = false;
            Result = default;

            var MatchParser = new RegexMatchParser() {
                Regex = Regex,
            };

            if (MatchParser.TryGetValue(Input, out var Matches)) {
                var TResult = new List<TResult>();

                foreach (var Match in Matches) {

                    if(TryGetValue(Match, out var TItem)) {
                        TResult.Add(TItem);
                    }

                }

                if(TResult.Count > 0) {
                    ret = true;
                    Result = TResult.ToImmutableList();
                }
            }



            return ret;
        }

    }


}
