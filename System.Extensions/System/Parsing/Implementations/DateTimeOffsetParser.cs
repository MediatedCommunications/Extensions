using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System {
    public record DateTimeOffsetParser : StructParser<DateTimeOffset> {
        public IFormatProvider? FormatProvider { get; init; }
        public DateTimeStyles Style {get; init ;}

        public override bool TryGetValue(string? Input, out DateTimeOffset Result) {
            return DateTimeOffset.TryParse(Input, FormatProvider, Style, out Result);
        }
    }

    public record RegexParser : DefaultClassParser<Regex> {
        public RegexOptions Options { get; init; } = RegularExpressions.Options;
        
        public override bool TryGetValue(string? Input, [NotNullWhen(true)] out Regex? Result) {
            var ret = false;
            Result = default;
            try {
                Result = new Regex(Input.Coalesce(), Options);
                ret = true;
            } catch(Exception ex) {
                ex.Ignore();
            }

            return ret;
        }

        protected override Regex GetDefaultValue() {
            return RegularExpressions.None;
        }
    }


}
