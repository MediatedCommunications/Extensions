using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace System
{
    public record MemoryStreamParser : DefaultClassParser<MemoryStream> {
        public Encoding Encoding { get; init; } = System.Text.Encoding.UTF8;


        public override bool TryGetValue(string? Input, [NotNullWhen(true)] out MemoryStream? Result) {
            Result = Encoding.GetBytes(Input.ToStringSafe()).ToMemoryStream();

            return true;
        }

        protected override MemoryStream GetDefaultValue() {
            return new MemoryStream();
        }

    }

}
