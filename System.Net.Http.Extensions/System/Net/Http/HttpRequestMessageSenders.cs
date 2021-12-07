using System.Net.Http.Message.Senders;

namespace System.Net.Http {
    public static class HttpRequestMessageSenders {
        public static DefaultHttpMessageSender Default { get; } = new();
        public static ThrowHttpMessageSender Throw { get; } = new();
    }

}
