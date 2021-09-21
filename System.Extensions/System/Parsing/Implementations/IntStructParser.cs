namespace System {
    public record IntStructParser : StructParser<int> {

        public override bool TryGetValue(out int Result) {
            return int.TryParse(Input, out Result);
        }
    }


}
