using System.Collections.Immutable;

namespace System.Text.Json.Serialization {
    public class ItemToListSerializer<TElement> : JsonConverter<ImmutableList<TElement>> {

        public override ImmutableList<TElement>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var ret = default(ImmutableList<TElement>);
            if(reader.TokenType == JsonTokenType.StartArray) {
                ret = JsonSerializer.Deserialize<ImmutableList<TElement>>(ref reader, options);
            } else {
                var tret = JsonSerializer.Deserialize<TElement>(ref reader, options);

                if(tret is { }) {
                    ret = ImmutableList<TElement>.Empty.Add(tret);
                }
               
            }

            return ret;
        }

        public override void Write(Utf8JsonWriter writer, ImmutableList<TElement> value, JsonSerializerOptions options) {
            JsonSerializer.Serialize(writer, value, options);
        }

    }

}

