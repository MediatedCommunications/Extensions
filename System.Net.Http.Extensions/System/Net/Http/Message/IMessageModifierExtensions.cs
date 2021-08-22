using System.Net.Http.Message.Senders;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message
{
    public static class IMessageModifierExtensions {

        public static async Task<HttpRequestMessage> ToMessageAsync(this IMessageModifier This) {
            var ret = new HttpRequestMessage();

            await This.ModifyAsync(ret)
                .DefaultAwait()
                ;

            return ret;
        }

        public static MessageModifierSender AsMessageSender(this IMessageModifier This) {
            var ret = new MessageModifierSender(This);

            return ret;
        }

    }

    public record MessageModifierSender : DelegatingMessageSender<IMessageModifier, HttpResponseMessage> {
        
        public IMessageModifier Modifier { get; init; }

        public MessageModifierSender(IMessageModifier Modifier, IMessageSender<IMessageModifier, HttpResponseMessage>? Child = null) : base(Child) {
            this.Modifier = Modifier;
        }

        public override Task<HttpResponseMessage> SendAsync(IMessageModifier Message, CancellationToken Token) {
            var NewMessage = Message.AsMessageBuilder()
                .Add(Modifier)
                ;

            return base.SendAsync(NewMessage, Token);
        }

    }

}
