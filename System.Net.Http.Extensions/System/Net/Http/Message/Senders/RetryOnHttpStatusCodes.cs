using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders
{
    public record RetryOnHttpStatusCodes : RetryDelegatingMessageSender {

        public ImmutableHashSet<HttpStatusCode> StatusCodes { get; init; } = ImmutableHashSet<HttpStatusCode>.Empty;


        public RetryOnHttpStatusCodes(IEnumerable<HttpStatusCode> StatusCodes, IEnumerable<TimeSpan>? Attempts, IHttpRequestMessageSender? Child = default) : base(Attempts, Child) {
            this.StatusCodes = StatusCodes.ToImmutableHashSet();
        }

        public override async Task<HttpResponseMessage> SendAsync(IHttpRequestMessageBuilder Message, CancellationToken Token) {
            var Index = -1;
            while (true) {
                var ret = await base.SendAsync(Message, Token)
                    .DefaultAwait()
                    ;

                if (StatusCodes.Contains(ret.StatusCode)) {
                    Index += 1;

                    if (Index < Delays.Count) {
                        await Task.Delay(Delays[Index], Token)
                            .DefaultAwait()
                            ;
                    } else {
                        throw new InvalidStatusCodeException(ret.StatusCode);
                    }
                } else {
                    return ret;
                }
            }
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(StatusCodes.Select(x => (object?)x).ToArray())
                ;
        }

    }

}
