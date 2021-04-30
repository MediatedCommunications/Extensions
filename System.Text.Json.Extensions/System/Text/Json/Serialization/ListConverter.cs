using System.Collections.Generic;
using System.Collections.Immutable;

namespace System.Text.Json.Serialization {

    /// <summary>
    /// Used to convert a list to another class type (usually a dictionary)
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TConvertTo"></typeparam>
    public abstract class ListConverter<TItem, TConvertTo> : JsonConverter<TConvertTo> {

        protected abstract TConvertTo FromList(ImmutableList<TItem> This);
        protected abstract LinkedList<TItem> ToList(TConvertTo This);

        public override TConvertTo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var ret = default(TConvertTo?);

            var tret = JsonSerializer.Deserialize<ImmutableList<TItem>>(ref reader, options);
            if (tret is { } V1) {
                ret = FromList(V1);
            }


            return ret;
        }

        public override void Write(Utf8JsonWriter writer, TConvertTo value, JsonSerializerOptions options) {
            if (value is { } V1) {
                var List = ToList(V1);
                JsonSerializer.Serialize(writer, List, options);
            }
        }

    }


}
