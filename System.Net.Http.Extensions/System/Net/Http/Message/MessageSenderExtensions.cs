using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http.Message.Senders;
using System.Linq;

namespace System.Net.Http.Message
{
    public static class MessageSenderExtensions {
        public static DelegatingMessageSender<IMessageModifier, HttpResponseMessage> RetryOnHttpException(this DelegatingMessageSender<IMessageModifier, HttpResponseMessage> This, IEnumerable<TimeSpan>? Attempts = default) {
            return This.Add(new RetryOnHttpException(Attempts));
        }

        public static DelegatingMessageSender<IMessageModifier, HttpResponseMessage> RetryOnHttpStatusCodes(this DelegatingMessageSender<IMessageModifier, HttpResponseMessage> This, IEnumerable<HttpStatusCode> StatusCodes, IEnumerable<TimeSpan>? Attempts = default) {
            return This.Add(new RetryOnHttpStatusCodes(StatusCodes, Attempts));
        }

        public static DelegatingMessageSender<IMessageModifier, HttpResponseMessage> FollowRedirects(this DelegatingMessageSender<IMessageModifier, HttpResponseMessage> This, Func<HttpResponseMessage, bool>? ShouldFollowRedirect = default, int? MaxRedirects = default) {
            return This.Add(new FollowRedirectsMessageSender(ShouldFollowRedirect, MaxRedirects));
        }

        public static ImmutableList<HttpStatusCode> HttpStatusErrors { get; private set; } = new[] {
            HttpStatusCode.Unauthorized,
            HttpStatusCode.TooManyRequests,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.BadGateway,
        }.ToImmutableList();

        public static DelegatingMessageSender<IMessageModifier, HttpResponseMessage> RetryOnHttpStatusErrors(this DelegatingMessageSender<IMessageModifier, HttpResponseMessage> This, IEnumerable<TimeSpan>? Attempts = default) {
            return RetryOnHttpStatusCodes(This, HttpStatusErrors, Attempts);
        }

        public static DelegatingMessageSender<IMessageModifier, HttpResponseMessage> SendUsingHttpClient(this DelegatingMessageSender<IMessageModifier, HttpResponseMessage> This, HttpClient? Client = default, HttpCompletionOption? CompletionOption = default) {
            return This.Add(new HttpClientMessageSender(Client, CompletionOption));
        }

        public static IEnumerable<IMessageSender<TRequest, TResponse>> AllChildren<TRequest, TResponse>(this IMessageSender<TRequest, TResponse> This) {
            var Current = This;
            while(Current is { }) {
                yield return Current;

                if(Current is DelegatingMessageSender<TRequest, TResponse> V1) {
                    Current = V1.Child;
                } else {
                    Current = default;
                }
            }

        }


        public static DelegatingMessageSender<TRequest, TResponse> Add<TRequest, TResponse>(this DelegatingMessageSender<TRequest, TResponse> This, IMessageSender<TRequest, TResponse> Child) {
            
            var Items = This
                .AllChildren()
                .OfType<DelegatingMessageSender<TRequest, TResponse>>()
                .Reverse()
                .ToList()
                ;


            Items[0] = Items[0] with {
                Child = Child
            };

            for (var i = 1; i < Items.Count; i++) {
                Items[i] = Items[i] with {
                    Child = Items[i - 1]
                };
            }

            var ret = Items.Last();

            return ret;
        }

    }
}
