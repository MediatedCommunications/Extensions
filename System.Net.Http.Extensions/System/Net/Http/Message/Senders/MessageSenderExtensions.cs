using System.Collections.Generic;

namespace System.Net.Http.Message.Senders {
    public static class MessageSenderExtensions {
        public static IEnumerable<IMessageSender<TRequest, TResponse>> GetEnumerable<TRequest, TResponse>(this IMessageSender<TRequest, TResponse> This) {
            var start = This;

            while(true) {
                yield return start;

                if(start is DelegatingMessageSender<TRequest, TResponse> { } V1) {
                    start = V1.Child;
                } else {
                    yield break;
                }

            }

        }
    }

}
