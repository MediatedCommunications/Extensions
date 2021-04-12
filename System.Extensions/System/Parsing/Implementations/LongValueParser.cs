namespace System {
    public record LongValueParser : StructParser<long> {
        public LongValueParser(string? Value) : base(Value) {
        }

        public override bool TryGetValue(out long Result) {
            return long.TryParse(Value, out Result);
        }
    }


}
