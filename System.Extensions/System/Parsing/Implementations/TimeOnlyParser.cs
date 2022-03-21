namespace System {
    public record TimeOnlyParser : StructParser<TimeOnly> {

        public override bool TryGetValue(string? Input, out TimeOnly Result) {
            return TimeOnly.TryParse(Input, out Result);
        }
    }

}
