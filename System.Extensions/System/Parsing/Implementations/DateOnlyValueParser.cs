namespace System {
    public record DateOnlyValueParser : StructParser<DateOnly> {

        public override bool TryGetValue(out DateOnly Result) {
            return DateOnly.TryParse(Input, out Result);
        }
    }

}
