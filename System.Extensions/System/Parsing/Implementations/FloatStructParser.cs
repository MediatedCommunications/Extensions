namespace System {
    public record FloatStructParser : StructParser<float> {

        public override bool TryGetValue(out float Result) {
            return float.TryParse(Input, out Result);
        }
    }


}
