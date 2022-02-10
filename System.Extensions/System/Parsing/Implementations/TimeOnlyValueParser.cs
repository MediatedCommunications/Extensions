namespace System {
    public record TimeOnlyValueParser : StructParser<TimeOnly> {

        public override bool TryGetValue(out TimeOnly Result) {
            return TimeOnly.TryParse(Input, out Result);
        }
    }

}
