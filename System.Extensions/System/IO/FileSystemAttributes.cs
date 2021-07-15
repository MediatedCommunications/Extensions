namespace System.IO {
    [Flags]
    public enum FileSystemAttributes {
        None = 0,
        Device = 1 << 1,
        Directory = 1 << 2,
        File = 1 << 3,
        
        Normal = 1 << 4,

        Hidden = 1 << 8,
        System = 1 << 9,
        ReadOnly = 1 << 10,
        Archive = 1 << 11,
        Temporary = 1 << 12,
        Compressed = 1 << 13,
        Encrypted = 1<< 14,
        Offline = 1 << 15,
        Empty = 1 << 16,
        EmptyTree = 1 << 17,

        SparseFile = 1 << 20,
        ReparsePoint = 1 << 21,
        NotContentIndexed = 1 << 22,
        IntegrityStream = 1 << 23,
        NoScrubData = 1 << 24,

    }

}
