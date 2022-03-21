using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace System {
    public record ValueNamedCaptureParser : RegexClassParser<string> {
        public string ValueField { get; init; } = KeyValueFields.Value;

        protected override bool TryGetValue(Match Input, [NotNullWhen(true)] out string? Result) {
            var ret = false;
            Result = default;

            if (Input.Groups.TryGetValue(ValueField, out var ValueGroup) && ValueGroup.Success) {
                ret = true;
                Result = ValueGroup.Value;
            } 

            return ret;
        }

    }

}
