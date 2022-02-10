using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace System.Text.Matching
{
    public record ExactMatchResult : MatchResult {

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Postfix.Add($@"{MatchedCount}")
                ;
        }

        public ImmutableList<ExactMatchSegment> Segments { get; init; } = ImmutableList<ExactMatchSegment>.Empty;

    }

    public record MatchSegment : DisplayRecord {
        public string Left { get; init; } = Strings.Empty;
        public string Right { get; init; } = Strings.Empty;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.AddExpression(Right, Left)
                ;
        }

    }

    public record ExactMatchSegment : MatchSegment {
        public bool IsMatch { get; init; }
    }

    public static class ExactMatchSegmentExtensions {
        public static string MatchedString(this IEnumerable<ExactMatchSegment> This, char? OptionalInvalidMatchChar = default) {
            var ret = new StringBuilder();

            var InvalidMatchChar = OptionalInvalidMatchChar ?? 'X';

            foreach (var Segment in This.EmptyIfNull()) {

                var Length = Math.Max(Segment.Left.Length, Segment.Right.Length);

                var ValueToAppend = Segment.IsMatch
                    ? Segment.Left
                    : new string(InvalidMatchChar, Length)
                    ;

                ret.Append(ValueToAppend);

            }


            return ret.ToString();
        }
    }
    

}
