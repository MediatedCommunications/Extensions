namespace System.Text.Json.Serialization {
    public class StringConverter : JsonConverter<string> {

        public static StringConverter Instance { get; private set; } = new StringConverter();

        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var ret = string.Empty;

            try {

                ret = reader.TokenType switch {
                    JsonTokenType.None => string.Empty,
                    JsonTokenType.Comment => reader.GetComment().Coalesce(),
                    JsonTokenType.String => reader.GetString().Coalesce(),
                    JsonTokenType.Number => reader.GetDouble().ToString(),
                    JsonTokenType.True => true.ToString(),
                    JsonTokenType.False => false.ToString(),
                    JsonTokenType.Null => string.Empty,
                    _ => string.Empty
                };
            } catch (Exception ex) {
                ex.Ignore();
            }

            return ret;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) {
            writer.WriteStringValue(value);
        }

    }
}
