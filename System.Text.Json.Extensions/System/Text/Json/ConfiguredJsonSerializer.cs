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

        public bool TryDeserialize<TValue>(string? Value, out TValue? ret) {
            var success = false;
            ret = default;
            try {
                ret = Deserialize<TValue>(Value);
            } catch (Exception ex) {
                ex.Ignore();
            }

            return success;
        }

        public bool TryDeserialize<TValue>(ref Utf8JsonReader Value, out TValue? ret) {
            var success = false;
            ret = default;
            try {
                ret = Deserialize<TValue>(ref Value);
            } catch (Exception ex) {
                ex.Ignore();
            }

            return success;
        }

        public bool TryDeserialize<TValue>(ReadOnlySpan<byte> Value, out TValue? ret) {
            var success = false;
            ret = default;
            try {
                ret = Deserialize<TValue>(Value);
            } catch (Exception ex) {
                ex.Ignore();
            }

            return success;
        }

        public TValue? TryDeserialize<TValue>(string? Value) {
            TryDeserialize(Value, out TValue? ret);
            return ret;
        }

        public TValue? TryDeserialize<TValue>(ref Utf8JsonReader Value) {
            TryDeserialize(ref Value, out TValue? ret);
            return ret;
        }

        public TValue? TryDeserialize<TValue>(ReadOnlySpan<byte> Value) {
            TryDeserialize(Value, out TValue? ret);
            return ret;
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

    }

}
