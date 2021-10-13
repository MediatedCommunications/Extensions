using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders
{
    public record FollowRedirectsMessageSender : DelegatingHttpMessageSender {
        public int MaxRedirects { get; init; }
        public Func<HttpResponseMessage, bool> ShouldFollowRedirect { get; init; }

        public static bool ShouldFollowLocation(HttpResponseMessage Response) {
            var ret = false;
            if (Response.StatusCode.IsRedirect()) {
                ret = true;
            }

            return ret;
        }

        public FollowRedirectsMessageSender(Func<HttpResponseMessage, bool>? ShouldFollowRedirect = default, int? MaxRedirects = default, IHttpMessageSender? Child = default) : base(Child) {
            this.MaxRedirects = MaxRedirects ?? 50;
            this.ShouldFollowRedirect = ShouldFollowRedirect ?? ShouldFollowLocation;
        }

        public override async Task<HttpResponseMessage> SendAsync(IHttpRequestMessageBuilder Message, CancellationToken Token) {
            var Original = await base.SendAsync(Message, Token)
                .DefaultAwait()
                ;

            var ret = Original;

            var Count = 0;
            while(Count < MaxRedirects && ret.Headers.Location is { } NewLocation && ShouldFollowRedirect(ret)) {
                var NewMessage = Message.AsMessageBuilder()
                    .Uri(NewLocation)
                    ;

                ret = await base.SendAsync(NewMessage, Token)
                    .DefaultAwait()
                    ;
            }

            return ret;
        }
    }
}