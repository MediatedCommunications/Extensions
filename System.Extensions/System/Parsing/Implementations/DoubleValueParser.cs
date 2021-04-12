namespace System {
    public record DoubleValueParser : StructParser<double> {
        public DoubleValueParser(string? Value) : base(Value) {
        }

        public override bool TryGetValue(out double Result) {
            return double.TryParse(Value, out Result);
        }
    }


}
