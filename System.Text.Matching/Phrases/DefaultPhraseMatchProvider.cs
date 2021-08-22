using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace System.Text.Matching
{
    public class DefaultPhraseMatchProvider : PhraseMatchProvider {
        public static DefaultPhraseMatchProvider Default { get; private set; } = new();

        public DefaultPhraseMatchProviderOptions Options { get; private set; }

        public DefaultPhraseMatchProvider(DefaultPhraseMatchProviderOptions? Options = default) {
            this.Options = Options ?? new();
        }


        private static IEnumerable<MatchResult> GetBestMatches(IEnumerable<MatchResult> Matches) {

            foreach (var item in Matches) {
                yield return item;

                if(item.Weight >= MatchResult.PerfectMatch) {
                    yield break;
                }

            }

        }

        public override PhraseMatchResult<TLeft, TRight> Match<TLeft, TRight>(TokenizeResult<TLeft> Left, TokenizeResult<TRight> Right, StringComparer Comparer) {
            var Matches = new List<MatchResult>();

            var Matchers = Options.Providers;

            var MyLeft = Left.Tokens;
            var MyRight = Right.Tokens;

            while (MyLeft.Count > 0 && MyRight.Count > 0) {
                var AllMatches = (
                    from z in Matchers
                    from x in MyLeft
                    from y in MyRight
                    let v = z.Match(x, y, Comparer)
                    where v.Weight > Options.MinimumWeight
                    select v
                    );

                var BestMatches = GetBestMatches(AllMatches).ToLinkedList();
                var BestMatch = (
                    from x in BestMatches orderby
                    x.Weight descending, x.MatchedCount descending
                    select x
                    ).FirstOrDefault();

                if (BestMatch is { }){
                    Matches.Add(BestMatch);
                    MyLeft = MyLeft.Remove(BestMatch.Left);
                    MyRight = MyRight.Remove(BestMatch.Right);
                } else {
                    break;
                }
            }

            var Weight = (
                from x in Matches
                let V = x.MatchedCount * x.Weight
                select V
                ).Sum();

            var ret = PhraseMatchResult.Create(Matches.ToImmutableList(), Weight, Left, Right);

            return ret;
        }

    }


}
