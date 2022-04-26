namespace System.Reflection {
    public static class EntryAssembly {
        public static string FullPath { get; } = Strings.Empty;
        public static string FolderPath { get; } = Strings.Empty;

        static EntryAssembly() {
            if (Assembly.GetEntryAssembly() is { } ASM) {
                FullPath = ASM.Location;
                FolderPath = IO.Path.GetDirectoryName(FullPath) ?? Strings.Empty;
            }
        }
    }
}
