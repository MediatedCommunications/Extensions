using System.Diagnostics;

namespace System.Reflection {

    public static class Entry {
        public static EntryLocation Assembly { get; }
        public static EntryLocation Process { get; }

        static Entry() {
            Assembly = new EntryLocation(System.Reflection.Assembly.GetEntryAssembly()?.Location);
            Process = new EntryLocation(System.Diagnostics.Process.GetCurrentProcess()?.MainModule?.FileName);
        }
    }

    public record EntryLocation : DisplayRecord {
        public string FullPath { get; init; } = Strings.Empty;
        public string FolderPath { get; init; } = Strings.Empty;

        public EntryLocation(string? FullPath) {
            if(FullPath is { } V1 && V1.IsNotBlank()) {
                this.FullPath = V1;
                this.FolderPath = IO.Path.GetDirectoryName(V1) ?? Strings.Empty;
            }
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(FullPath)
                ;
        }

    }

}
