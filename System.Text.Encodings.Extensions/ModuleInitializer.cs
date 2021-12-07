using System.Runtime.CompilerServices;

namespace System.Text.Encodings.Extensions {
    static internal class ModuleInitializer {
        [ModuleInitializer]
        static internal void Initialize() {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

    }

}
