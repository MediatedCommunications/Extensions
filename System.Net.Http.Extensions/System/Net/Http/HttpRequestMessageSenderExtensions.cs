using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http.Message.Senders;
using System.Linq;
using System.Net.Http.Message;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public static class HttpRequestMessageSenderExtensions {

        public static MessageModifierSender AsMessageSender(this IHttpRequestMessageBuilder This) {
            var ret = new MessageModifierSender(This);

            return ret;
        }

        public static DelegatingHttpMessageSender RetryOnHttpException(this DelegatingHttpMessageSender This, IEnumerable<TimeSpan>? Attempts = default) {
            return This.Add(new RetryOnHttpException(Attempts));
        }

        public static DelegatingHttpMessageSender RetryOnHttpStatusCodes(this DelegatingHttpMessageSender This, IEnumerable<HttpStatusCode> StatusCodes, IEnumerable<TimeSpan>? Attempts = default) {
            return This.Add(new RetryOnHttpStatusCodes(StatusCodes, Attempts));
        }

        public static DelegatingHttpMessageSender FollowRedirects(this DelegatingHttpMessageSender This, Func<HttpResponseMessage, bool>? ShouldFollowRedirect = default, int? MaxRedirects = default) {
            return This.Add(new FollowRedirectsMessageSender(ShouldFollowRedirect, MaxRedirects));
        }

        public static ImmutableList<HttpStatusCode> HttpStatusErrors { get; private set; } = new[] {
            HttpStatusCode.Unauthorized,
            HttpStatusCode.TooManyRequests,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.GatewayTimeout,
            HttpStatusCode.InsufficientStorage,
        }.ToImmutableList();

        public static DelegatingHttpMessageSender RetryOnHttpStatusErrors(this DelegatingHttpMessageSender This, IEnumerable<TimeSpan>? Attempts = default) {
            return RetryOnHttpStatusCodes(This, HttpStatusErrors, Attempts);
        }

        public static DelegatingHttpMessageSender SendUsingHttpClient(this DelegatingHttpMessageSender This, HttpClient? Client = default, HttpCompletionOption? CompletionOption = default) {
            return This.Add(new HttpClientMessageSender(Client, CompletionOption));
        }

        public static IEnumerable<IHttpRequestMessageSender> AllChildren(this IHttpRequestMessageSender This) {
            var Current = This;
            while(Current is { }) {
                yield return Current;

                if(Current is DelegatingHttpMessageSender V1) {
                    Current = V1.Child;
                } else {
                    Current = default;
                }
            }

        }


        public static DelegatingHttpMessageSender Add(this DelegatingHttpMessageSender This, IHttpRequestMessageSender Child) {
            
            var Items = This
                .AllChildren()
                .OfType<DelegatingHttpMessageSender>()
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
