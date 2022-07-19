namespace System.Data {
    public static class DatabaseKey {
        public static T Default<T>() {
            return DatabaseKey<T>.Default;
        }
    }
}