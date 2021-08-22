using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders
{
    public record HttpClientMessageSender : MessageSender<IMessageModifier, HttpResponseMessage> {
        public HttpClient Client { get; init; }
        public HttpCompletionOption CompletionOption { get; init; }

        public HttpClientMessageSender(HttpClient? Client = default, HttpCompletionOption? CompletionOption = default) {
            this.Client = Client ?? new HttpClient();
            this.CompletionOption = CompletionOption ?? HttpCompletionOption.ResponseHeadersRead;
        }

        public override async Task<HttpResponseMessage> SendAsync(IMessageModifier Message, CancellationToken Token) {
            var Request = await Message.ToMessageAsync()
                .DefaultAwait()
                ;

            var ret = await Client.SendAsync(Request, CompletionOption, Token)
                .DefaultAwait()
                ;

            return ret;
        }

    }

}
