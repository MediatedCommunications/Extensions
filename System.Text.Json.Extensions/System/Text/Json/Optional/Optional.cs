using System.Collections.Generic;

namespace System.Text.Json {
    public static class Optional {
        public static Optional<T> Missing<T>() {
            return Optionals<T>.Missing;
        }

        public static Optional<T> Create<T>(T? Value) {
            return new Optional<T>(Value);
        }
    }

    public static class Optionals<T> {
        public static Optional<T> Missing { get; }

        static Optionals() {
            Missing = new();
        }
    }

}
