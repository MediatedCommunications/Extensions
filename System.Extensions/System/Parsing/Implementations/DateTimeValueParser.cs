namespace System {
    public record DateTimeValueParser : StructParser<DateTime> {
        
        public override bool TryGetValue(out DateTime Result) {
            return DateTime.TryParse(Input, out Result);
        }
    }

}
