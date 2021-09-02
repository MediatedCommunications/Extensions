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

    public class Null {
        static Null() {
            Type = typeof(Null);
            Instance = new();
        }

        public static Type Type { get; } 
        public static Null Instance { get; }
    }

    public class Null<T> : Null {
        static Null() {
            Type = typeof(Null<T>);
            Instance = new();
        }

        public static new Type Type { get; }
        public static new Null<T> Instance { get; }

    }


}
