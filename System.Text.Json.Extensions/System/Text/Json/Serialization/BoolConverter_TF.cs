using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace System.Text.Json.Serialization {
    public class BoolConverter_TF : JsonConverter<bool> {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var Value = reader.GetString();

            var ret = Value.AsText().Equals("T");

            return ret;
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) {
            var NewValue = value ? "T" : "F";

            writer.WriteStringValue(NewValue);
        }
    }

}
