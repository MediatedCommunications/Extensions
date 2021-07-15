using System.Runtime.CompilerServices;
using System.Text;

namespace RtfPipe {
    static internal class ModuleInitializer {
        [ModuleInitializer]
        static internal void Initialize() {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

    }
}
