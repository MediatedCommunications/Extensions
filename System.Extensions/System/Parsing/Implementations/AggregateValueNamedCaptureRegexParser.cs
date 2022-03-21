using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace System {

    public delegate string AggregateValueNamedCaptureRegexParserAggregator(List<string> Values, Dictionary<string, Group> Groups);

    public static class AggregateValueNamedCaptureRegexParserAggregators {
        public static string Join(List<string> Values, Dictionary<string, Group> Groups) {
            var ret = Values.Join();

            return ret;
        }

        public static string TrimJoin(List<string> Values, Dictionary<string, Group> Groups) {
            var ret = Values.Trim().Join();

            return ret;
        }

    }

    public record AggregateValueNamedCaptureRegexParser : RegexClassParser<string> {
        public ImmutableList<string> ValueFields { get; init; } = KeyValueFields.Values1;
        public bool RequireAll { get; init; } = true;
        public AggregateValueNamedCaptureRegexParserAggregator Aggregator { get; init; } = AggregateValueNamedCaptureRegexParserAggregators.Join;

        protected override bool TryGetValue(Match Input, [NotNullWhen(true)] out string? Result) {
            var ret = false;
            Result = default;

            var ResultParts = new List<string>();
            var ResultValues = new Dictionary<string, Group>();
            var AllMatched = true;

            foreach (var ValueField in ValueFields) {
                if (Input.Groups.TryGetValue(ValueField, out var ValueGroup) && ValueGroup.Success) {
                    var Value = ValueGroup.Value;
                    ResultParts.Add(Value);
                    ResultValues[ValueField] = ValueGroup;
                } else {
                    AllMatched = false;
                }

            }

            if (!RequireAll || AllMatched) {
                ret = true;
                Result = Aggregator(ResultParts, ResultValues);
            }




            return ret;
        }

    }

}
