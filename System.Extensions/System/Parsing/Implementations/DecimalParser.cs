namespace System {
    public record DecimalParser : StructParser<decimal> {
        
        public override bool TryGetValue(string? Input, out decimal Result) {
            return decimal.TryParse(Input, out Result);
        }
    }


}
