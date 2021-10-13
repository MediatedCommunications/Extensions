namespace System.Net.Http.Message.Senders {
    public static class HttpMessageSenders {
        public static DefaultHttpMessageSender Default { get; } = new();
        public static ThrowHttpMessageSender Throw { get; } = new();
    }

}
