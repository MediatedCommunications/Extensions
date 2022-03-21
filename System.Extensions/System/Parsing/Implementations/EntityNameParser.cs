using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace System {
    public record EntityNameParser : DefaultClassParser<EntityName> {
        public ImmutableArray<PersonNameFormat> Formats { get; init; } = PersonNameFormats.All;

        protected override EntityName GetDefaultValue() {
            return EntityNames.Empty;
        }

        public override bool TryGetValue(string? Input, [NotNullWhen(true)] out EntityName? Result) {
            Result = EntityName.Parse(Input, Formats);

            return true;
        }
    }
}
