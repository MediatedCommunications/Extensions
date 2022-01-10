using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace System {
    public record KeyValueNamedCaptureRegexParser : RegexClassParser<KeyValuePair<string, string>> {
        public KeyValueNamedCaptureRegexParser() : base(ClassParser) {

        }

        static bool ClassParser(Match Input, [NotNullWhen(true)] out KeyValuePair<string, string> Result) {
            var ret = false;
            Result = default;

            var KeyKey = nameof(KeyValuePair<string, string>.Key);
            var ValueKey = nameof(KeyValuePair<string, string>.Value);

            if (Input.Groups.TryGetValue(KeyKey, out var KeyGroup) && Input.Groups.TryGetValue(ValueKey, out var ValueGroup)) {

                var Key = KeyGroup.Value;
                var Value = ValueGroup.Value;

                Result = KeyValuePair.Create(Key, Value);
                ret = true;
            }

            return ret;
        }

    }


}
