namespace System.Net.Http.Message.Senders {

    public record DefaultHttpMessageSender : DelegatingHttpMessageSender {
        public DefaultHttpMessageSender(IHttpRequestMessageSender? Child = default) : base(Child) {
        }

    }

}
