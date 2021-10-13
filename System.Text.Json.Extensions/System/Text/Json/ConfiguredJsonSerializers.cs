using System.Text.Json.Serialization;

namespace System.Text.Json {
    public static class ConfiguredJsonSerializers {
        public static ConfiguredJsonSerializer Default { get; }
        public static ConfiguredJsonSerializer NumericEnums { get; }
        public static ConfiguredJsonSerializer NamedEnums { get; }

        static ConfiguredJsonSerializers() {
            {
                var Options = new JsonSerializerOptions() {
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = true,
                    Converters = {

                    },
                };
                Default = new(Options);
            }

            {
                var Options = new JsonSerializerOptions() {
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = true,
                    Converters = {
                    
                    },
                };
                NumericEnums = new(Options);
            }

            {
                var Options = new JsonSerializerOptions() {
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = true,
                    Converters = {
                        new JsonStringEnumMemberConverter(),
                    },
                };
                NamedEnums = new(Options);
            }

        }
    }

}
