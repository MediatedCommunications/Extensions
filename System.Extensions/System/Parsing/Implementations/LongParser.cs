namespace System {
    public record LongParser : StructParser<long> {

        public override bool TryGetValue(string? Input, out long Result) {
            return long.TryParse(Input, out Result);
        }
    }


}
