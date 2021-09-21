namespace System {
    public record LongStructParser : StructParser<long> {

        public override bool TryGetValue(out long Result) {
            return long.TryParse(Input, out Result);
        }
    }


}
