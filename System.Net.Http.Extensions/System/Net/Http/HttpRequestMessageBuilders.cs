namespace System.Net.Http {
    public static class HttpRequestMessageBuilders {
        public static HttpRequestMessageBuilder Default { get; private set; } = new HttpRequestMessageBuilder();
    }

}
