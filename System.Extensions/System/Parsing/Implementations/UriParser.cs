using System.Diagnostics.CodeAnalysis;

namespace System {
    public record UriParser : ClassParser<Uri> {

        public override bool TryGetValue(string? Input, [NotNullWhen(true)] out Uri? Result) {
            return Uri.TryCreate(Input, UriKind.Absolute, out Result);
        }
    }


}
