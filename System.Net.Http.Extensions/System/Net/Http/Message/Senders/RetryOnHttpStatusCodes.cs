using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders {
    public record RetryOnHttpStatusCodes : RetryDelegatingMessageSender {

        public ImmutableList<HttpStatusCode> StatusCodes { get; init; } = ImmutableList<HttpStatusCode>.Empty;


        public RetryOnHttpStatusCodes(IEnumerable<HttpStatusCode> StatusCodes, IEnumerable<TimeSpan>? Attempts, IMessageSender<IMessageModifier, HttpResponseMessage>? Child = default) : base(Attempts, Child) {
            this.StatusCodes = StatusCodes.ToImmutableList();
        }

        public override async Task<HttpResponseMessage> SendAsync(IMessageModifier Message, CancellationToken Token) {
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
