using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace System {
    public abstract record RegexClassParser<TResult> : ListParser<TResult> {
        protected Regex Regex { get; init; }
        private TryGetValue<Match, TResult> ClassParser { get; init; }
        
        public RegexClassParser(Regex Regex, TryGetValue<Match, TResult> ClassParser, string? Value) : base(Value) {
            this.Regex = Regex;
            this.ClassParser = ClassParser;
        }

        public override bool TryGetValue([NotNullWhen(true)] out LinkedList<TResult>? Result) {
            var ret = false;
            Result = default;

            var MatchParser = new RegexMatchParser(Regex, Input);

            if (MatchParser.TryGetValue(out var Matches)) {
                var TResult = new LinkedList<TResult>();

                foreach (var Match in Matches) {

                    if(ClassParser(Match, out var TItem)) {
                        TResult.Add(TItem);
                    }

                }

                if(TResult.Count > 0) {
                    ret = true;
                    Result = TResult;
                }
            }



            return ret;
        }

    }


}
