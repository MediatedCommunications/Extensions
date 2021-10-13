namespace System.Net.Http.Message.Senders {

    public record DefaultHttpMessageSender : DelegatingHttpMessageSender {
        public DefaultHttpMessageSender(IHttpMessageSender? Child = default) : base(Child) {
        }

    }

}
