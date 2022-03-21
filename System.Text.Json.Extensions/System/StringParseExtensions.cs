using System.Text.Json;

namespace System {
    public static class StringParseExtensions { 

        public static ClassParserContext<JsonValueParser<TJson>, TJson> AsJson<TJson>(this ParseValue This, ConfiguredJsonSerializer? Serializer = default) where TJson : class {
            var Parser = new JsonValueParser<TJson>() {
                Serializer = Serializer ?? ConfiguredJsonSerializers.Default
            };

            return ClassParserContext.Create<JsonValueParser<TJson>, TJson>(This, Parser);

        }

    }


}
