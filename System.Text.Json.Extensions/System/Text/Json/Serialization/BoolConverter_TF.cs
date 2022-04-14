namespace System.Text.Json.Serialization {
    public class BoolConverter_TF : JsonConverter<bool> {

        public static string ConvertFromValue(bool Value) {
            var ret = Value ? "T" : "F";

            return ret;
        }
        public static bool ConvertFromString(string? Value) {
            var ret = Value.AsText().Equals("T");

            return ret;
        }


        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var Value = reader.GetString();

            var ret = ConvertFromString(Value);

            return ret;
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) {
            var ret = ConvertFromValue(value);

            writer.WriteStringValue(ret);
        }
    }

}
