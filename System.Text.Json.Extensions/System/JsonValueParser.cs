using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace System {
    public record JsonValueParser<TJson> : ClassParser<TJson> where TJson : class {
        public ConfiguredJsonSerializer Serializer { get; init; }

        public JsonValueParser(string? Value, ConfiguredJsonSerializer? Serializer = default) : base(Value) {
            this.Serializer = Serializer ?? ConfiguredJsonSerializer.Default;
        }

        public override bool TryGetValue([NotNullWhen(true)] out TJson? Result) {
            var ret = false;
            Result = default;

            try {
                if (Value is { }) {
                    Result = Serializer.Deserialize<TJson>(Value);
                    ret = true;
                }
            } catch (Exception ex) {
                ex.Ignore();
            }


            return ret;
        }
    }
}
