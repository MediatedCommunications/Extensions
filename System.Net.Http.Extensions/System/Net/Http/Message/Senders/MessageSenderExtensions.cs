using System.Collections.Generic;

namespace System.Net.Http.Message.Senders
{
    public static class MessageSenderExtensions {
        public static IEnumerable<IHttpMessageSender> GetEnumerable<TRequest, TResponse>(this IHttpMessageSender This) {
            var start = This;

            while(true) {
                yield return start;

                if(start is DelegatingHttpMessageSender { } V1) {
                    start = V1.Child;
                } else {
                    yield break;
                }

            }

        }
    }

}
