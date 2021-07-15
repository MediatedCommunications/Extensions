namespace System {
    public record IntValueParser : StructParser<int> {
        public IntValueParser(string? Value) : base(Value) {
        
        }

        public override bool TryGetValue(out int Result) {
            return int.TryParse(Input, out Result);
        }
    }


}
