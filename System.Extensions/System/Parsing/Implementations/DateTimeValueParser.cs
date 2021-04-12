namespace System {
    public record DateTimeValueParser : StructParser<DateTime> {
        public DateTimeValueParser(string? Value) : base(Value) {
        }

        public override bool TryGetValue(out DateTime Result) {
            return DateTime.TryParse(Value, out Result);
        }
    }

}
