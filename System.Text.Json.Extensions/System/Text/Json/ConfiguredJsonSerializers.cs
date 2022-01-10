using System.Text.Json.Serialization;

namespace System.Text.Json {
    public static class Converters {
        public static JsonConverter StringEnumConverter() {
            return new JsonStringEnumMemberConverter();
        }
    }

    public static class ConfiguredJsonSerializers {
        public static ConfiguredJsonSerializer Default { get; }
        public static ConfiguredJsonSerializer NumericEnums { get; }
        public static ConfiguredJsonSerializer NamedEnums { get; }

        static ConfiguredJsonSerializers() {
            {
                var Options = new JsonSerializerOptions() {
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    Converters = {

                    },
                };
                Default = new(Options);
            }

            {
                var Options = new JsonSerializerOptions() {
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    Converters = {
                    
                    },
                };
                NumericEnums = new(Options);
            }

            {
                var Options = new JsonSerializerOptions() {
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    Converters = {
                        Converters.StringEnumConverter(),
                    },
                };
                NamedEnums = new(Options);
            }

        }
    }

}
