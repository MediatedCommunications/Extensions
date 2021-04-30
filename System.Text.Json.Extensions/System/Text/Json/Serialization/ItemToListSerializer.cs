using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Text.Json.Serialization {
    public class ItemToListSerializer : JsonConverterFactory {
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) {
            var ret = default(JsonConverter?);
            if (GetTypes(typeToConvert, out var CollectionType, out var ElementType)) {
                var NewType = typeof(ItemToListSerializer<>).MakeGenericType(ElementType);
                ret = Activator.CreateInstance(NewType) as JsonConverter;
            } 

            if(ret is null) {
                throw new NotImplementedException();
            }

            return ret;
            
        }

        private bool GetTypes(Type TypeToConvert, [NotNullWhen(true)] out Type? CollectionType, [NotNullWhen(true)] out Type? ElementType) {
            var ret = false;

            CollectionType = default;
            ElementType = default;

            if (TypeToConvert.IsGenericType && TypeToConvert.GetGenericArguments() is { } Args && Args.Length == 1 && Args.First() is { } InnerType) {
                ret = true;
                CollectionType = TypeToConvert;
                ElementType = InnerType;
            }

            return ret;
        }

        public override bool CanConvert(Type typeToConvert) {
            var ret = false;
            if (GetTypes(typeToConvert, out _, out _)) {
                ret = true;
            }


            return ret;
        }
    }

}

