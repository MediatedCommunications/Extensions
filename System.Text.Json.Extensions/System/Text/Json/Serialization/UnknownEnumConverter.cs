using System.Reflection;

namespace System.Text.Json.Serialization {
    public class UnknownEnumConverter<T> : JsonConverter<T> where T : struct {
        private static readonly JsonSerializerOptions Options;
        static UnknownEnumConverter() {
            var BaseOptions = new JsonSerializerOptions() {

            };

            BaseOptions.Converters.Add(new JsonStringEnumConverter());

            Options = BaseOptions;

        }

        public UnknownEnumConverter(string? Default) {
            if (Enum.TryParse<T>(Default, out var V1)) {
                this.Default = V1;
            }

        }

        public UnknownEnumConverter(T Default = default) {
            this.Default = Default;
        }

        protected T Default { get; set; }


        public override bool CanConvert(Type typeToConvert) {
            return typeToConvert == typeof(T);
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var ret = Default;

            try {
                var Value = reader.GetString();

                ret = Value.Parse().AsEnum<T>().GetValue(Default);

            } catch (Exception ex) {
                ex.Ignore();

            }

            return ret;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) {
            JsonSerializer.Serialize(writer, value, Options);
        }

    }

    public class UnknownEnumConverter : JsonConverterFactory {

        protected string Default { get; set; }

        /// <summary>
        /// Constructor. Creates the <see cref="UnknownEnumConverter"/> with the
        /// default naming policy and allows integer values.
        /// </summary>
        public UnknownEnumConverter(string? Default = default) {
            this.Default = Default ?? nameof(Default);
        }

        /// <inheritdoc />
        public override bool CanConvert(Type typeToConvert) {
            return typeToConvert.IsEnum;
        }

        /// <inheritdoc />
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) {
            var converter = (JsonConverter)Activator.CreateInstance(
                typeof(UnknownEnumConverter<>).MakeGenericType(typeToConvert),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                new object?[] { Default },

                culture: null)!;

            return converter;
        }
    }
}
