using System.Globalization;

namespace System {
    public record DateTimeOffsetValueParser : StructParser<DateTimeOffset> {
        public IFormatProvider? FormatProvider { get; init; }
        public DateTimeStyles Style {get; init ;}

        public DateTimeOffsetValueParser(string? Value, DateTimeStyles Style = default, IFormatProvider? FormatProvider = default) : base(Value) {
            this.FormatProvider = FormatProvider;
            this.Style = Style;
        }

        public override bool TryGetValue(out DateTimeOffset Result) {
            return DateTimeOffset.TryParse(Value, FormatProvider, Style, out Result);
        }
    }


}
