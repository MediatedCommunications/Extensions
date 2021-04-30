namespace System.Text.Matching {
    public class HammingMatchProvider : MatchProvider {
        public static HammingMatchProvider Instance { get; private set; } = new HammingMatchProvider();

        public override HammingMatchResult Match(string Left, string Right, StringComparer? OptionalComparer = default) {
            var Comparer = Defaults.GetComparer(OptionalComparer);

            var LeftLetters = Defaults.GetLetters(Left);
            var RightLetters = Defaults.GetLetters(Right);

            var Distance = LevenshteinDistance.Compute(LeftLetters, RightLetters, Comparer);
            var MatchedCount = Math.Max(0, LeftLetters.Count - Distance);
            var Weight = 0.0;

            if (Distance > 0 && MatchedCount > 0) {
                var Top = (double)(LeftLetters.Count + RightLetters.Count - Distance);
                var Bottom = (double)(LeftLetters.Count + RightLetters.Count);

                if (LeftLetters.Count == 0) {
                    Weight = 0;
                } else {
                    Weight = Math.Max(0.0, Top / Bottom);
                }
            }

            var ret = new HammingMatchResult() {
                Left = Left,
                Right = Right,
                Distance = Distance,
                UnmatchedCount = Distance,
                MatchedCount = MatchedCount,
                Weight = Weight,
                
            };

            return ret;
        }

    }
}
