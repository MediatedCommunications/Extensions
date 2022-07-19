namespace System.Data {
    public static class DatabaseKey<TKey> {
        public static TKey Default { get; }

        static DatabaseKey() {
            if(typeof(TKey) == typeof(string)) {
                Default = (TKey)(object)Strings.Empty;
            } else if (typeof(TKey) == typeof(object)) {
                Default = (TKey)Type.Missing;
            } else {
                Default = default(TKey) ?? throw new NullReferenceException(nameof(TKey));
            }
        }
    }
}