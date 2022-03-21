namespace System {
    public record IntParser : StructParser<int> {

        public override bool TryGetValue(string? Input, out int Result) {
            return int.TryParse(Input, out Result);
        }
    }


}
