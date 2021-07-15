using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System {
    public record DateTimeOffsetValueParser : StructParser<DateTimeOffset> {
        public IFormatProvider? FormatProvider { get; init; }
        public DateTimeStyles Style {get; init ;}

        public DateTimeOffsetValueParser(string? Value, DateTimeStyles Style = default, IFormatProvider? FormatProvider = default) : base(Value) {
            this.FormatProvider = FormatProvider;
            this.Style = Style;
        }

        public override bool TryGetValue(out DateTimeOffset Result) {
            return DateTimeOffset.TryParse(Input, FormatProvider, Style, out Result);
        }
    }

    public record RegexParser : DefaultClassParser<Regex> {
        public RegexOptions Options { get; init; }

        public RegexParser(string? Value, RegexOptions Options) : base(Value) {
            this.Options = Options;
        }
        
        public override bool TryGetValue([NotNullWhen(true)] out Regex? Result) {
            var ret = false;
            Result = default;
            try {
                Result = GetValue();
                ret = true;
            } catch(Exception ex) {
                ex.Ignore();
            }

            return ret;
        }

        public override Regex GetValue() {
            return new Regex(this.Input.Coalesce(), Options);
        }
    }


}
