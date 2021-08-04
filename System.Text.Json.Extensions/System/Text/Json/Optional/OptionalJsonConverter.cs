using System.Text.Json.Serialization;

namespace System.Text.Json {
    public class OptionalJsonConverter<T> : JsonConverter<Optional<T>> {
        public override Optional<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {

            var RawValue = (T?) JsonSerializer.Deserialize(ref reader, typeof(T), options);

            var ret = new Optional<T>(RawValue);

            return ret;
        }

        public override void Write(Utf8JsonWriter writer, Optional<T> value, JsonSerializerOptions options) {
            if (value.IsPresent) {
                JsonSerializer.Serialize(writer, value.Value, options);
            }
        }
    }


}
