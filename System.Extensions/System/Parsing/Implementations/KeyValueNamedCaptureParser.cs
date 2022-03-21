using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace System {
    public record KeyValueNamedCaptureParser : RegexClassParser<KeyValuePair<string, string>> {
        public string KeyField { get; init; } = KeyValueFields.Key;
        public string ValueField { get; init; } = KeyValueFields.Key;
        
        protected override bool TryGetValue(Match Input, [NotNullWhen(true)] out KeyValuePair<string, string> Result) {
            var ret = false;
            Result = default;

            if (true
                && Input.Groups.TryGetValue(KeyField, out var KeyGroup) 
                && KeyGroup.Success

                && Input.Groups.TryGetValue(ValueField, out var ValueGroup)
                && ValueGroup.Success

                ) {

                var Key = KeyGroup.Value;
                var Value = ValueGroup.Value;

                Result = KeyValuePair.Create(Key, Value);
                ret = true;
            }

            return ret;
        }

    }

}
