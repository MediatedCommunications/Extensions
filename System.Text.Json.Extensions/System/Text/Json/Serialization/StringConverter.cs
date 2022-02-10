namespace System.Text.Json.Serialization {
    public class StringConverter : JsonConverter<string> {

        public static StringConverter Instance { get; private set; } = new StringConverter();

        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var ret = Strings.Empty;

            try {

                ret = reader.TokenType switch {
                    JsonTokenType.None => Strings.Empty,
                    JsonTokenType.Comment => reader.GetComment().Coalesce(),
                    JsonTokenType.String => reader.GetString().Coalesce(),
                    JsonTokenType.Number => reader.GetDouble().ToString(),
                    JsonTokenType.True => true.ToString(),
                    JsonTokenType.False => false.ToString(),
                    JsonTokenType.Null => Strings.Empty,
                    _ => Strings.Empty
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
