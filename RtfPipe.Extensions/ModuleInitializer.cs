﻿using System.Runtime.CompilerServices;

namespace RtfPipe {
    static internal class ModuleInitializer {
        [ModuleInitializer]
        static internal void Initialize() {
            System.Text.Encodings.Extensions.Activator.EnsureReference();
        }

    }

}
