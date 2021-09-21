namespace System {
    public record DecimalStructParser : StructParser<decimal> {
        
        public override bool TryGetValue(out decimal Result) {
            return decimal.TryParse(Input, out Result);
        }
    }


}
