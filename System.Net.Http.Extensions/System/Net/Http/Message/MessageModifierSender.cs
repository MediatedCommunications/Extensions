using System.Net.Http.Message.Senders;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message
{

    public record MessageModifierSender : DelegatingHttpMessageSender {
        
        public IHttpRequestMessageBuilder Modifier { get; init; }

        public MessageModifierSender(IHttpRequestMessageBuilder Modifier, IHttpRequestMessageSender? Child = null) : base(Child) {
            this.Modifier = Modifier;
        }

        public override Task<HttpResponseMessage> SendAsync(IHttpRequestMessageBuilder Message, CancellationToken Token) {
            var NewMessage = Message.AsMessageBuilder()
                .Add(Modifier)
                ;

            return base.SendAsync(NewMessage, Token);
        }

    }

}
