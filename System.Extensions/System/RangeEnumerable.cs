using System.Diagnostics;

namespace System.Collections.Generic {
    public record RangeEnumerable 
        : DisplayRecord
        , IEnumerable<int> {
        public Range Range { get; init; }
        public RangeEndpoint StartIs { get; init; } = RangeEndpoint.Inclusive;
        public RangeEndpoint EndIs { get; init; } = RangeEndpoint.Exclusive;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Range)
                .Postfix.If(StartIs != RangeEndpoint.Inclusive).Add($@"{nameof(StartIs)}:{StartIs}")
                .Postfix.If(StartIs != RangeEndpoint.Exclusive).Add($@"{nameof(EndIs)}:{EndIs}")
                ;
        }

        public IEnumerator<int> GetEnumerator() {
            var LastValue = int.MaxValue;

            var Start = Range.Start.GetOffset(LastValue);
            var End = Range.End.GetOffset(LastValue);

            var Step = Start < End
                ? 1
                : -1
                ;

            if (StartIs == RangeEndpoint.Exclusive) {
                Start += Step;
            }

            if (EndIs == RangeEndpoint.Exclusive) {
                End -= Step;
            }

            Func<int, int, bool> Condition = Step > 0
                ? (x, y) => x <= y
                : (x, y) => x >= y
                ;

            {
                var i = Start;
                while (Condition(i, End)) {
                    yield return i;
                    i += Step;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

}
