using System.Net.Http.Message.Senders;

namespace System.Net.Http {
    public static class HttpRequestMessageSenders {
        public static DefaultHttpMessageSender Default { get; }
        public static ThrowHttpMessageSender Throw { get; }

        static HttpRequestMessageSenders() {
            Throw = new ThrowHttpMessageSender();
            Default = new DefaultHttpMessageSender(Throw);
        }
    }

}
