using System.Text.Json.Serialization;

namespace System.Text.Json {
    public class OptionalJsonConverterFactory : JsonConverterFactory {
        public override bool CanConvert(Type typeToConvert) {
            var ret = false;

            if(typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Optional<>)) {
                ret = true;
            }

            return ret;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) {

            var TypeOfT = typeToConvert.GetGenericArguments()[0];
            var ConverterType = typeof(OptionalJsonConverter<>).MakeGenericType(TypeOfT);

            var ret = Activator.CreateInstance(ConverterType) as JsonConverter
                ?? throw new NullReferenceException()
                ;

            return ret;
        }
    }


}
