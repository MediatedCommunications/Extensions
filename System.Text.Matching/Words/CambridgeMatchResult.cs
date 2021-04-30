using System.Collections.Immutable;
using System.Diagnostics;

namespace System.Text.Matching {

    public record CambridgeMatchResult : MatchResult {
        public bool FirstMatches { get; init; }
        public bool LastMatches { get; init; }
        public bool FirstAndLastMatches => FirstMatches && LastMatches;
        public ImmutableHashSet<string> LeftUnique { get; init; } = ImmutableHashSet<string>.Empty;
        public ImmutableHashSet<string> RightUnique { get; init; } = ImmutableHashSet<string>.Empty;
        public ImmutableHashSet<string> Unmatched { get; init; } = ImmutableHashSet<string>.Empty;
        public ImmutableHashSet<string> Matched { get; init; } = ImmutableHashSet<string>.Empty;


        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Postfix.AddIf(FirstMatches, "FirstMatches", "!FirstMatches")
                .Postfix.AddIf(LastMatches, "LastMatches", "!LastMatches")
                ;
        }


    }

}
