using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace System {
    public record EntityNameParser : DefaultClassParser<EntityName> {
        public ImmutableArray<PersonNameFormat> Formats { get; init; } = ImmutableArray<PersonNameFormat>.Empty;

        public override EntityName GetValue() {
            var ret = (EntityName) EntityNames.Empty;
            if(TryGetValue(out var tret)) {
                ret = tret;
            }

            return ret;
        }

        public override bool TryGetValue([NotNullWhen(true)] out EntityName? Result) {
            Result = EntityName.Parse(Input, Formats);

            return true;
        }
    }
}
