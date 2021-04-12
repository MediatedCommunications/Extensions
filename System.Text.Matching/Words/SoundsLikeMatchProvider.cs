using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Matching {
    public class SoundsLikeMatchProvider : MatchProvider {
        public static SoundsLikeMatchProvider Instance { get; private set; } = new SoundsLikeMatchProvider();

        public override SoundsLikeMatchResult Match(string Left, string Right, StringComparer? OptionalComparer = default) {
            var LeftCode = Metaphone.Encode(Left);
            var RightCode = Metaphone.Encode(Right);

            var EM = HammingMatchProvider.Instance.Match(LeftCode, RightCode, OptionalComparer);

            var ret = new SoundsLikeMatchResult() {
                Left = Left,
                LeftCode = LeftCode,

                Right = Right,
                RightCode = RightCode,
                
                MatchedCount = EM.MatchedCount,
                UnmatchedCount = EM.UnmatchedCount,
                Weight = EM.Weight,
            };



            return ret;
        }
    }
}
