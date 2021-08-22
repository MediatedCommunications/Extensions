using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace System.Text.Matching
{
    public class CambridgeMatchProvider : MatchProvider {

        public static CambridgeMatchProvider Instance { get; private set; } = new CambridgeMatchProvider();

        public override CambridgeMatchResult Match(string Left, string Right, StringComparer? OptionalComparer = default) {
            var Comparer = Defaults.GetComparer(OptionalComparer);

            var v1 = Defaults.GetLetters(Left);
            var v2 = Defaults.GetLetters(Right);

            var FMatch = Comparer.Equals(v1.FirstOrDefault(), v2.FirstOrDefault());
            var LMatch = Comparer.Equals(v1.LastOrDefault(), v2.LastOrDefault());

            //Left Inner Chars
            var LChars = ImmutableList<string>.Empty;
            if(v1.Count >= 3) {
                LChars = v1.GetRange(1..^1).ToImmutableList();
            }
            var LeftLetters = LChars.Select(x => x.ToString()).ToImmutableHashSet(Comparer);


            //Right Inner Chars
            var RChars = ImmutableList<string>.Empty;
            if (v2.Count >= 3) {
                RChars = v2.GetRange(1..^1).ToImmutableList();
            }
            var RightLetters = RChars.Select(x => x.ToString()).ToImmutableHashSet(Comparer);

            //Compute Left Uniques

            var LeftUnique = LeftLetters.Except(RightLetters).ToImmutableHashSet(Comparer);
            var RightUnique = RightLetters.Except(LeftLetters).ToImmutableHashSet(Comparer);
            var Unmatched = new LinkedList<string>() { LeftUnique, RightUnique }.ToImmutableHashSet(Comparer);
            var Matched = LeftLetters.Intersect(RightLetters);

            var MatchedCount = Matched.Count + (FMatch ? 1 : 0) + (LMatch ? 1 : 0);
            var UnmatchedCount = Unmatched.Count + (LMatch ? 0 : 1) + (LMatch ? 0 : 1);

            var Numerator = MatchedCount * (0.0
                    + (FMatch ? 0.5 : 0.0)
                    + (LMatch ? 0.5 : 0.0)
                );
            var Denominator = MatchedCount + UnmatchedCount;

            var Weight = Denominator == 0
                ? 1.0
                : Numerator / Denominator
                ;

            var ret = new CambridgeMatchResult() {
                Left = Left,
                Right = Right,

                FirstMatches = FMatch,
                LastMatches = LMatch,

                LeftUnique = LeftUnique,
                RightUnique = RightUnique,
                
                Matched = Matched,
                Unmatched = Unmatched,

                MatchedCount = MatchedCount,
                UnmatchedCount = UnmatchedCount,

                Weight = Weight,
            };

            return ret;

        }
        
    }

}
