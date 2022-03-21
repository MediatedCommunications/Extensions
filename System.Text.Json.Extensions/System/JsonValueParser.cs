using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace System {
    public record JsonValueParser<TJson> : ClassParser<TJson> where TJson : class {
        public ConfiguredJsonSerializer Serializer { get; init; } = ConfiguredJsonSerializers.Default;

        public override bool TryGetValue(string? Input, [NotNullWhen(true)] out TJson? Result) {
            var ret = false;
            Result = default;

            try {
                if (Input is { }) {
                    Result = Serializer.Deserialize<TJson>(Input);
                    ret = true;
                }
            } catch (Exception ex) {
                ex.Ignore();
            }


            return ret;
        }
    }
}
