namespace System {
    public record DateTimeParser : StructParser<DateTime> {
        
        public override bool TryGetValue(string? Input, out DateTime Result) {
            return DateTime.TryParse(Input, out Result);
        }
    }

}
