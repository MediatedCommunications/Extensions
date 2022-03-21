namespace System {
    public record DoubleParser : StructParser<double> {

        public override bool TryGetValue(string? Input, out double Result) {
            return double.TryParse(Input, out Result);
        }
    }


}
