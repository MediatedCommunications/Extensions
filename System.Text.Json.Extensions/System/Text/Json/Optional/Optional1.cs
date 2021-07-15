using System.Text.Json.Serialization;

namespace System.Text.Json {
    [JsonConverter(typeof(OptionalJsonConverterFactory))]
    public struct Optional<T> 
        : IOptional {
        public bool IsPresent { get; }
        public T? Value { get; }

        public static Optional<T> NotPresent => default;

        bool IOptional.IsPresent => IsPresent;
        object? IOptional.Value => Value;

        public Optional(T? Value) {
            this.Value = Value;
            this.IsPresent = true;
        }

        public static implicit operator Optional<T>(T? Value) {
            return new Optional<T>(Value);
        }

        public static explicit operator T?(Optional<T> Optional) {
            var ret = default(T);
            if (Optional.IsPresent) {
                ret = Optional.Value;
            }

            return ret;
        }

        public bool TryGetValue(out T? Value) {
            Value = this.Value;

            return IsPresent;
        }

    }


}
