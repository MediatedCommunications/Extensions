namespace System {
    public record FloatValueParser : StructParser<float> {
        public FloatValueParser(string? Value) : base(Value) {
        }

        public override bool TryGetValue(out float Result) {
            return float.TryParse(Input, out Result);
        }
    }


}
