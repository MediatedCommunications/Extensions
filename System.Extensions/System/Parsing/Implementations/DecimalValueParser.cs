namespace System {
    public record DecimalValueParser : StructParser<decimal> {
        public DecimalValueParser(string? Value) : base(Value) {
        }

        public override bool TryGetValue(out decimal Result) {
            return decimal.TryParse(Value, out Result);
        }
    }


}
