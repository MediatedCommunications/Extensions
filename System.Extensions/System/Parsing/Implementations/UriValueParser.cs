using System.Diagnostics.CodeAnalysis;

namespace System {
    public record UriValueParser : ClassParser<Uri> {
        public UriValueParser(string? Value) : base(Value) {
        }

        public override bool TryGetValue([NotNullWhen(true)] out Uri? Result) {
            return Uri.TryCreate(Input, UriKind.Absolute, out Result);
        }
    }


}
