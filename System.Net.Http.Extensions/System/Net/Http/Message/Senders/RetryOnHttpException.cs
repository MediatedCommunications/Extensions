using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders
{
    public record RetryOnHttpException : RetryDelegatingMessageSender {

        public RetryOnHttpException(IEnumerable<TimeSpan>? Attempts, IMessageSender<IMessageModifier, HttpResponseMessage>? Child = default) : base(Attempts, Child) {

        }

        public override async Task<HttpResponseMessage> SendAsync(IMessageModifier Message, CancellationToken Token) {
            var Index = -1;
            while (true) {
                try {

                    var ret = await base.SendAsync(Message, Token)
                        .DefaultAwait()
                        ;

                    return ret;
                } catch(Exception ex) {
                    ex.Ignore();

                    Index += 1;

                    if (Index < Delays.Count) {
                        await Task.Delay(Delays[Index], Token)
                            .DefaultAwait()
                            ;
                    } else {
                        throw;
                    }
                }
            }
        }

    }

}
