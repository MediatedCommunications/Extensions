using System.IO;

namespace System.Text.Json {
    public class ConfiguredJsonSerializer {
        private readonly JsonSerializerOptions Options;

        public ConfiguredJsonSerializer(JsonSerializerOptions Options) {
            this.Options = Options;
        }


        public string Serialize(object Value, Type Type) {
            return JsonSerializer.Serialize(Value, Type, Options);
        }

        public string Serialize<TValue>(TValue Value) {
            return JsonSerializer.Serialize(Value, Options);
        }

        public TValue? Deserialize<TValue>(string? Value) {
            var ret = default(TValue?);
            if (Value.IsNotBlank()) {
                ret = JsonSerializer.Deserialize<TValue>(Value, Options);
            }
            return ret;
        }

        public object? Deserialize(string? Value, Type Type) {
            var ret = default(object?);
            if (Value.IsNotBlank()) {
                ret = JsonSerializer.Deserialize(Value, Type, Options);
            }
            return ret;
        }

        public object? Deserialize(JsonDocument? Value, Type Type) {
            var ret = default(object?);
            if (Value is { }) {
                ret = JsonSerializer.Deserialize(Value, Type, Options);
            }
            return ret;
        }


        public TValue? TryDeserialize<TValue>(string? Value) {
            TryDeserialize(Value, out TValue? ret);
            return ret;
        }

        public bool TryDeserialize<TValue>(string? Value, out TValue? ret) {
            var success = false;
            ret = default;
            try {
                ret = Deserialize<TValue>(Value);
                success = true;
            } catch (Exception ex) {
                ex.Ignore();
            }

            return success;
        }



        public void Serialize(Utf8JsonWriter Writer, object Value, Type Type) {
            JsonSerializer.Serialize(Writer, Value, Type, Options);
        }

        public void Serialize<T>(Utf8JsonWriter Writer, T Value) {
            JsonSerializer.Serialize(Writer, Value, Options);
        }


        public TValue? Deserialize<TValue>(ref Utf8JsonReader Value) {
            return JsonSerializer.Deserialize<TValue>(ref Value, Options);
        }

        public TValue? TryDeserialize<TValue>(ref Utf8JsonReader Value) {
            TryDeserialize(ref Value, out TValue? ret);
            return ret;
        }

        public bool TryDeserialize<TValue>(ref Utf8JsonReader Value, out TValue? ret) {
            var success = false;
            ret = default;
            try {
                ret = Deserialize<TValue>(ref Value);
                success = true;
            } catch (Exception ex) {
                ex.Ignore();
            }

            return success;
        }





        public void Serialize<T>(Stream Stream, T Value) {
            JsonSerializer.Serialize(Stream, Value, Options);
        }

        public TValue? Deserialize<TValue>(Stream Value) {
            return JsonSerializer.Deserialize<TValue>(Value, Options);
        }

        public TValue? TryDeserialize<TValue>(Stream Value) {
            TryDeserialize(Value, out TValue? ret);
            return ret;
        }

        public bool TryDeserialize<TValue>(Stream Value, out TValue? ret) {
            var success = false;
            ret = default;
            try {
                ret = Deserialize<TValue>(Value);
                success = true;
            } catch (Exception ex) {
                ex.Ignore();
            }

            return success;
        }

        public TValue? Deserialize<TValue>(ReadOnlySpan<byte> Value) {
            return JsonSerializer.Deserialize<TValue>(Value, Options);
        }

        public TValue? TryDeserialize<TValue>(ReadOnlySpan<byte> Value) {
            TryDeserialize(Value, out TValue? ret);
            return ret;
        }

        public bool TryDeserialize<TValue>(ReadOnlySpan<byte> Value, out TValue? ret) {
            var success = false;
            ret = default;
            try {
                ret = Deserialize<TValue>(Value);
                success = true;
            } catch (Exception ex) {
                ex.Ignore();
            }

            return success;
        }




        public TValue? Deserialize<TValue>(JsonDocument? Value) {
            var ret = default(TValue?);
            if (Value is { } V1) {
                ret = V1.Deserialize<TValue>(Options);
            }
            return ret;
        }

        public TValue? TryDeserialize<TValue>(JsonDocument? Value) {
            TryDeserialize(Value, out TValue? ret);
            return ret;
        }

        public bool TryDeserialize<TValue>(JsonDocument? Value, out TValue? ret) {
            var success = false;
            ret = default;
            try {
                ret = Deserialize<TValue>(Value);
                success = true;
            } catch (Exception ex) {
                ex.Ignore();
            }

            return success;
        }

       





        






        public void SerializeToFile<T>(string FullPath, T Value) {
            using var FS = System.IO.File.Create(FullPath);
            Serialize(FS, Value);
        }

        public T? DeserializeFromFile<T>(string FullPath) {
            using var FS = System.IO.File.OpenRead(FullPath);
            var ret = Deserialize<T>(FS);

            return ret;
        }

        public bool TryDeserializeFromFile<T>(string FullPath, out T? ret) {
            var Success = false;
            ret = default;
            try {
                ret = DeserializeFromFile<T>(FullPath);
                Success = true;
            } catch (Exception ex) {
                ex.Ignore();
            }


            return Success;
        }

    }

}
