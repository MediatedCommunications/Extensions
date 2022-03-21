namespace System {
    public record DateOnlyParser : StructParser<DateOnly> {

        public override bool TryGetValue(string? Input, out DateOnly Result) {
            return DateOnly.TryParse(Input, out Result);
        }
    }

}
