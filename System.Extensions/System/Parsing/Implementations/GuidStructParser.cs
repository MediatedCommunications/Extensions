namespace System {
    public record GuidStructParser : StructParser<Guid> {

        public override bool TryGetValue(out Guid Result) {
            return Guid.TryParse(Input, out Result);
        }
    }


}
