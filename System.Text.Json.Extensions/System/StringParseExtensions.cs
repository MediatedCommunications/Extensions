using System.Text.Json;

namespace System {
    public static class StringParseExtensions { 

        public static JsonValueParser<TJson> AsJson<TJson>(this ParseValue This, ConfiguredJsonSerializer? Serializer = default) where TJson : class {
            return new JsonValueParser<TJson>() {
                Input = This.Value,
                Serializer = Serializer ?? ConfiguredJsonSerializers.Default
            };
        }

    }


}
