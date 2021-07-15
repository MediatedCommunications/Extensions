namespace System.Text.Json {
    public static class Optional {
        public static Optional<T> Create<T>(T Value) {
            return new Optional<T>(Value);
        }
    }


}
