﻿using System.Net.Http.Message;
using System.Net.Http.Message.Senders;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders {
    public record FollowRedirectsMessageSender : DelegatingMessageSender<IMessageModifier, HttpResponseMessage> {
        public int MaxRedirects { get; init; }
        public Func<HttpResponseMessage, bool> ShouldFollowRedirect { get; init; }

        public static bool ShouldFollowLocation(HttpResponseMessage Response) {
            var ret = false;
            if (Response.StatusCode.IsRedirect()) {
                ret = true;
            }

            return ret;
        }

        public FollowRedirectsMessageSender(Func<HttpResponseMessage, bool>? ShouldFollowRedirect = default, int? MaxRedirects = default, IMessageSender<IMessageModifier, HttpResponseMessage>? Child = default) : base(Child) {
            this.MaxRedirects = MaxRedirects ?? 50;
            this.ShouldFollowRedirect = ShouldFollowRedirect ?? ShouldFollowLocation;
        }

        public override async Task<HttpResponseMessage> SendAsync(IMessageModifier Message, CancellationToken Token) {
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