using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace System {
    public record MemoryStreamParser : DefaultClassParser<MemoryStream> {
        public Encoding Encoding { get; init; }

        public MemoryStreamParser(Encoding Encoding, string? Value) : base(Value) {
            this.Encoding = Encoding;
        }

        public override bool TryGetValue([NotNullWhen(true)] out MemoryStream? Result) {
            Result = Encoding.GetBytes(Value.ToStringSafe()).ToMemoryStream();

            return true;
        }

        public override MemoryStream GetValue() {
            return GetValue(new MemoryStream());
        }

    }

}
