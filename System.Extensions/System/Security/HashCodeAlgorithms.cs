namespace System.Security {
    public static class HashCodeAlgorithms {
        public static HashCodeAlgorithm SHA1 { get; }
        public static HashCodeAlgorithm MD5 { get; }

        static HashCodeAlgorithms() {
            SHA1 = new HashCodeAlgorithm(() => Cryptography.SHA1.Create());
            MD5 = new HashCodeAlgorithm(() => Cryptography.MD5.Create());
        }
    }
}
