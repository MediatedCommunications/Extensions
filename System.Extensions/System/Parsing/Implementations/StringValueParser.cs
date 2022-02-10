using System.Diagnostics.CodeAnalysis;

namespace System {
    public record StringValueParser : DefaultClassParser<string> {

        public override bool TryGetValue([NotNullWhen(true)] out string? Result) {
            bool ret;

            (Result, ret) = Input is { } V1
                ? (V1, true)
                : (default, false)
                ;

            return ret;
        }

        public override string GetValue() {
            return GetValue(Strings.Empty);
        }
    }


}
