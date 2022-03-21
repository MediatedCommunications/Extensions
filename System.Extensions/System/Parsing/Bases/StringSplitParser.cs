using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace System {
    public record StringSplitParser : ListParser<string> {
        public StringSplitOptions Options { get; init; } = StringSplitOptions.RemoveEmptyEntries;
        public string Delimiter { get; init; } = Strings.Space;
        public int MaxSplits { get; init; } = int.MaxValue;
        public Func<IEnumerable<string>, IEnumerable<string>> Filter { get; init; } = (x => x);
        public bool AllowEmpty { get; init; } = false;

        public override bool TryGetValue(string? Input, [NotNullWhen(true)] out ImmutableList<string>? Result) {
            var ret = false;
            Result = default;

            if(Input is { } V1) {
                var Splits = V1.Split(Delimiter, MaxSplits, Options);
                var Values = Filter(Splits).ToImmutableList();

                if(AllowEmpty || !Values.IsEmpty) {
                    ret = true;
                    Result = Values;
                }

            }


            return ret;
        }
    }

}
