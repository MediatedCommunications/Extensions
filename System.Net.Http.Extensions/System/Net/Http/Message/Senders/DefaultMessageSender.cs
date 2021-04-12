namespace System.Net.Http.Message.Senders {
    public static class DefaultMessageSender {
        
        public static DefaultMessageSender<TRequest, TResponse> Create<TRequest, TResponse>(IMessageSender<TRequest, TResponse>? Child = default) {
            return DefaultMessageSender<TRequest, TResponse>.Create(Child);
        }

        public static DefaultMessageSender<IMessageModifier, HttpResponseMessage> CreateHttp(IMessageSender<IMessageModifier, HttpResponseMessage>? Child = default) {
            return new DefaultMessageSender<IMessageModifier, HttpResponseMessage>(Child);
        }

    }

    public record DefaultMessageSender<TRequest, TResponse> : DelegatingMessageSender<TRequest, TResponse> {
        public DefaultMessageSender(IMessageSender<TRequest, TResponse>? Child = default) : base(Child) {
        }

        public static DefaultMessageSender<TRequest, TResponse> Create(IMessageSender<TRequest, TResponse>? Child = default) {
            return new DefaultMessageSender<TRequest, TResponse>(Child);
        }

    }

}
