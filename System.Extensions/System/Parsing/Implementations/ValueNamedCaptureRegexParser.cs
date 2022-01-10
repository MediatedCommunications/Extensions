using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace System {
    public record ValueNamedCaptureRegexParser : RegexClassParser<string> {
        public ValueNamedCaptureRegexParser() : base(ClassParser) {

        }

        static bool ClassParser(Match Input, [NotNullWhen(true)] out string? Result) {
            var ret = false;
            Result = default;

            var ValueKey = nameof(KeyValuePair<string, string>.Value);

            if (Input.Groups.TryGetValue(ValueKey, out var ValueGroup)) { 
                var Value = ValueGroup.Value;

                Result = Value;
                ret = true;
            }

            return ret;
        }

    }


}
