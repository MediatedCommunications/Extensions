using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace System {
    public record KeyValueNamedCaptureRegexParser : RegexClassParser<KeyValuePair<string, string>> {
        public string KeyField { get; init; } = KeyValueFields.Key;
        public string ValueField { get; init; } = KeyValueFields.Key;
        
        protected override bool TryGetValue(Match Input, [NotNullWhen(true)] out KeyValuePair<string, string> Result) {
            var ret = false;
            Result = default;

            if (Input.Groups.TryGetValue(KeyField, out var KeyGroup) && Input.Groups.TryGetValue(ValueField, out var ValueGroup)) {

                var Key = KeyGroup.Value;
                var Value = ValueGroup.Value;

                Result = KeyValuePair.Create(Key, Value);
                ret = true;
            }

            return ret;
        }

    }

    public static class KeyValueFields {
        public static string Key { get; }
        public static string Value { get; }

        static KeyValueFields() {
            Key = nameof(KeyValuePair<string, string>.Key);
            Value = nameof(KeyValuePair<string, string>.Value);
        }
    }

}
