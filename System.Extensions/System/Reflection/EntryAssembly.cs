namespace System.Reflection {
    public static class EntryAssembly {
        public static string FullPath { get; private set; } = Strings.Empty;
        public static string FolderPath { get; private set; } = Strings.Empty;

        static EntryAssembly() {
            if (Assembly.GetEntryAssembly() is { } ASM) {
                FullPath = ASM.Location;
                FolderPath = IO.Path.GetDirectoryName(FullPath) ?? Strings.Empty;
            }
        }
    }
}
