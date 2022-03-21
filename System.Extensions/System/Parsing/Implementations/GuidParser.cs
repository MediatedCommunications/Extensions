namespace System {
    public record GuidParser : StructParser<Guid> {

        public override bool TryGetValue(string? Input, out Guid Result) {
            return Guid.TryParse(Input, out Result);
        }
    }


}
