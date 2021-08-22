using System.Diagnostics;

namespace System.Text.Matching {
    public abstract record MatchResult 
        : DisplayRecord
        , IComparable<MatchResult> {
        public string Left { get; init; } = string.Empty;
        public string Right { get; init; } = string.Empty;

        public long MatchedCount { get; init; }
        public long UnmatchedCount { get; init; }

        public static double PerfectMatch => 1.0;

        public double Weight { get; init; } = PerfectMatch;
        public double RelativeWeight { get; init; } = PerfectMatch;

        

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.AddPair(Right, Left)
                .Postfix.Add($@"{Weight} / {RelativeWeight}")
                ;
        }


        public int CompareTo(MatchResult? other) {
            return CompareTo(this, other);
        }

        private static int CompareTo(object? LeftO, object? RightO) {
            var Left = LeftO as MatchResult;
            var Right = RightO as MatchResult;

            if (ReferenceEquals(Left, Right)) {
                return 0;
            } else if (Left == null) {
                return -1;
            } else if (Right == null) {
                return 1;
            } else if (Left.Weight < Right.Weight) {
                return -1;
            } else if (Left.Weight > Right.Weight) {
                return +1;
            } else {
                return 0;
            }
        }

        public static bool operator <(MatchResult left, MatchResult right) {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        public static bool operator <=(MatchResult left, MatchResult right) {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator >(MatchResult left, MatchResult right) {
            return left is not null && left.CompareTo(right) > 0;
        }

        public static bool operator >=(MatchResult left, MatchResult right) {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
    }
}
