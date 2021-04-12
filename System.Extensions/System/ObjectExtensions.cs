namespace System {
    public static class ObjectExtensions {
        public static void Ignore<T>(this T ex) {
            ex?.Equals(ex);
        }

        public static ParseValue Parse(this object? This) {
            return new ParseValue(This.ToStringSafe());
        }

    }

}
