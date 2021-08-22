using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace System
{
    public record KeyValueRegexParser : RegexClassParser<KeyValuePair<string, string>> {
        public KeyValueRegexParser(Regex Regex, string? Value) : base(Regex, ClassParser, Value) {

        }

        static bool ClassParser(Match Input, [NotNullWhen(true)] out KeyValuePair<string, string> Result) {
            var ret = false;
            Result = default;

            var KeyKey = nameof(KeyValuePair<string, string>.Key);
            var ValueKey = nameof(KeyValuePair<string, string>.Value);

            if (Input.Groups.ContainsKey(KeyKey) && Input.Groups.ContainsKey(ValueKey)) {

                var Key = Input.Groups[KeyKey].Value;
                var Value = Input.Groups[ValueKey].Value;

                Result = KeyValuePair.Create(Key, Value);
                ret = true;
            }

            return ret;
        }

    }


}
