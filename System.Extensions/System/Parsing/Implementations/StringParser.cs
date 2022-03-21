using System.Diagnostics.CodeAnalysis;

namespace System {
    public record StringParser : DefaultClassParser<string> {

        public override bool TryGetValue(string? Input, [NotNullWhen(true)] out string? Result) {
            bool ret;

            (Result, ret) = Input is { } V1
                ? (V1, true)
                : (default, false)
                ;

            return ret;
        }

        protected override string GetDefaultValue() {
            return Strings.Empty;
        }
    }


}
