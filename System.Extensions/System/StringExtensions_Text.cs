namespace System {
    public static class StringExtensions_Text {
        public static RegionalizedString AsText(this string? This) {
            return new RegionalizedString() {
                Value = This,
                Comparison = StringComparison.InvariantCultureIgnoreCase
            };
        }

        
    }

}
