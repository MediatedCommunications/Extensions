namespace System {
    public record FloatParser : StructParser<float> {

        public override bool TryGetValue(string? Input, out float Result) {
            return float.TryParse(Input, out Result);
        }
    }


}
