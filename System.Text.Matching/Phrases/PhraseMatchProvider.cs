using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace System.Text.Matching
{
    public abstract class PhraseMatchProvider {
        public PhraseMatchResult Match(string Left, string Right, StringComparer? OptionalComparer = default, Tokenizer? OptionalTokenizer = default) {
            var Tokenizer = Defaults.GetTokenizer(OptionalTokenizer);

            var LeftTokens = Tokenizer.Tokenize(Left);
            var RightTokens = Tokenizer.Tokenize(Right);

            var Comparer = Defaults.GetComparer(OptionalComparer);

            return Match(LeftTokens, RightTokens, Comparer);

        }

        public ImmutableList<PhraseMatchResult<TLeft, TRight>> Match<TLeft, TRight>(TokenizeResult<TLeft> Left, IEnumerable<TokenizeResult<TRight>> Right, StringComparer Comparer) {
            var SW = System.Diagnostics.Stopwatch.StartNew();
            var ret = (
                from x in Right.AsParallel()
                let MyMatch = Match(Left, x, Comparer)
                orderby MyMatch.Weight descending
                select MyMatch
                ).ToImmutableList();

            SW.Stop();

            return ret;

        }

        public abstract PhraseMatchResult<TLeft, TRight> Match<TLeft, TRight>(TokenizeResult<TLeft> Left, TokenizeResult<TRight> Right, StringComparer Comparer);

    }


}
