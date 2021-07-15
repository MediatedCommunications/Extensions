namespace System {
    public static class ObjectExtensions {
        public static void Ignore<T>(this T ex) {
            ex?.Equals(ex);
        }

        public static ParseValue Parse(this object? This) {
            return new ParseValue(This.ToStringSafe());
        }

        public static Type GetTypeSafe(this object? This) {
            var ret = This?.GetType() ?? Null.Type;

            return ret;
        }

    }

    internal class Null {
        internal static Type Type { get; } = typeof(Null);
    }

}
