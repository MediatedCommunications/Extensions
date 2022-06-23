using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace System {
    public record PersonNameParser : DefaultClassParser<PersonName> {
        public ImmutableArray<PersonNameFormat> Formats { get; init; } = PersonNameFormats.All;

        protected override PersonName GetDefaultValue() {
            return EntityNames.EmptyPerson;
        }

        public override bool TryGetValue(string? Input, [NotNullWhen(true)] out PersonName? Result) {
            var ret = false;
            Result = default;

            foreach (var Format in Formats) {
                if(Format.TryParse(Input ?? Strings.Empty, out var value)){
                    ret = true;
                    Result = value;
                }
            }
            

            return ret;
        }
    }
}
