using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace System.Text.Matching
{


    public class ExactMatchProvider : MatchProvider {

        public static ExactMatchProvider Instance { get; private set; } = new ExactMatchProvider();


        private static bool IsMatch(ImmutableList<string> LeftLetters, ImmutableList<string> RightLetters, StringComparer Comparer, int Position) {
            var ret = true
                && Position < LeftLetters.Count
                && Position < RightLetters.Count
                && Comparer.Equals(LeftLetters[Position], RightLetters[Position])
                ;

            return ret;
        }

        public override ExactMatchResult Match(string Left, string Right, StringComparer? OptionalComparer = default) {
            var Comparer = Defaults.GetComparer(OptionalComparer);

            var LeftLetters = Defaults.GetLetters(Left);
            var RightLetters = Defaults.GetLetters(Right);

            var Segments = new List<ExactMatchSegment>();

            {
                var Start = 0;
                var End = Math.Max(LeftLetters.Count, RightLetters.Count);

                while(Start < End) {

                    var RegionMatch = IsMatch(LeftLetters, RightLetters, Comparer, Start);
                    var RegionStart = Start;
                    var RegionEnd = (RegionStart..End)
                        .AsEnumerable()
                        .TakeWhile(x => IsMatch(LeftLetters, RightLetters, Comparer, x) == RegionMatch)
                        .LastOrDefault()
                        ;


                    var LeftPart = (
                        from x in (RegionStart..RegionEnd).AsEnumerable().EndIs(RangeEndpoint.Inclusive)
                        where x < LeftLetters.Count
                        select LeftLetters[x]
                        ).Join();

                    var RightPart = (
                        from x in (RegionStart..RegionEnd).AsEnumerable().EndIs(RangeEndpoint.Inclusive)
                        where x < RightLetters.Count
                        select RightLetters[x]
                        ).Join();

                    Segments.Add(new ExactMatchSegment() {
                        Left = LeftPart,
                        Right = RightPart,
                        IsMatch = RegionMatch,
                    });

                    Start += RegionEnd + 1;    

                }

            }

            var MatchedCount = Segments.Where(x => x.IsMatch).Select(x => Math.Max(x.Left.Length, x.Right.Length)).Sum();
            var UnmatchedCount = Segments.Where(x => !x.IsMatch).Select(x => Math.Max(x.Left.Length, x.Right.Length)).Sum();

            var Weight = 1.0;
            {
                var Numerator = (double)(MatchedCount);
                var Denominator = (double)(MatchedCount + UnmatchedCount);
                if(Denominator > 0) {
                    Weight = Numerator / Denominator;
                }
            }
            
            
            
            var ret = new ExactMatchResult() {
                Left = Left,
                Right = Right,
                Segments = Segments.ToImmutableList(),
                MatchedCount = MatchedCount,
                UnmatchedCount = UnmatchedCount,
                Weight = Weight,
            };

            return ret;

        }

    }
}
