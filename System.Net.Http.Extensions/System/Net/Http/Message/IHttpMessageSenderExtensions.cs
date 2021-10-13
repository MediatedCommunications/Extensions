using System.Threading.Tasks;

namespace System.Net.Http.Message {
    public static class IHttpMessageSenderExtensions {
        public static Task<HttpResponseMessage> SendAsync(this IHttpMessageSender This, IHttpRequestMessageBuilder Message) {
            return This.SendAsync(Message, default);
        }
    }

}
