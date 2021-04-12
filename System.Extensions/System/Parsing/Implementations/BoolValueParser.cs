namespace System {
    public record BoolValueParser : StructParser<bool> {
        public BoolValueParser(string? Value) : base(Value) {
        
        }

        public override bool TryGetValue(out bool Result) {
            return bool.TryParse(Value, out Result);
        }
    }


}
