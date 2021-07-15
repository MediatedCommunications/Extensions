namespace System {
    public record GuidValueParser : StructParser<Guid> {
        public GuidValueParser(string? Value) : base(Value) {
        
        }

        public override bool TryGetValue(out Guid Result) {
            return Guid.TryParse(Input, out Result);
        }
    }


}
