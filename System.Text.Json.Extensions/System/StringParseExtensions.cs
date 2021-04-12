using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace System {
    public static class StringParseExtensions { 

        public static JsonValueParser<TJson> AsJson<TJson>(this ParseValue This, ConfiguredJsonSerializer? Serializer = default) where TJson : class {
            return new JsonValueParser<TJson>(This.Value, Serializer);
        }

    }


}
