namespace System.Text.Json {
    public class ConfiguredJsonSerializer {
        private readonly JsonSerializerOptions Options;

        public ConfiguredJsonSerializer(JsonSerializerOptions Options) {
            this.Options = Options;
        }

        public TValue? Deserialize<TValue>(string? Value) {
            var ret = default(TValue?);
            if (Value.IsNotBlank()) {
                ret = JsonSerializer.Deserialize<TValue>(Value, Options);
            }
            return ret;
        }

        public TValue? Deserialize<TValue>(ref Utf8JsonReader Value) {
            return JsonSerializer.Deserialize<TValue>(ref Value, Options);
        }

        public TValue? Deserialize<TValue>(ReadOnlySpan<byte> Value) {
            return JsonSerializer.Deserialize<TValue>(Value, Options);
        }

        public string Serialize(object Value, Type Type) {
            return JsonSerializer.Serialize(Value, Type, Options);
        }

        public void Serialize(Utf8JsonWriter Writer, object Value, Type Type) {
            JsonSerializer.Serialize(Writer, Value, Type, Options);
        }

        public string Serialize<T>(T Value) {
            return JsonSerializer.Serialize(Value, Options);
        }

        public void Serialize<T>(Utf8JsonWriter Writer, T Value) {
            JsonSerializer.Serialize(Writer, Value, Options);
        }


        static ConfiguredJsonSerializer() {
            Default = new ConfiguredJsonSerializer(DefaultOptions());
        }

        public static ConfiguredJsonSerializer Default { get; private set; } 

        private static JsonSerializerOptions DefaultOptions() {
            var ret = new JsonSerializerOptions() {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
            };

            return ret;
        }


    }
}
