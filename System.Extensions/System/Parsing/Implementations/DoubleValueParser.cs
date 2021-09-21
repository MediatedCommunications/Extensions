namespace System {
    public record DoubleValueParser : StructParser<double> {

        public override bool TryGetValue(out double Result) {
            return double.TryParse(Input, out Result);
        }
    }


}
