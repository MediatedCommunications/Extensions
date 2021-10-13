using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Message;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http {
    public static class HttpClientExtensions {
        public static async Task<HttpResponseMessage> SendAsync(this HttpClient This, IHttpRequestMessageBuilder Builder) {
            var Message = await Builder.ToMessageAsync()
                .DefaultAwait()
                ;

            var ret = await This.SendAsync(Message)
                .DefaultAwait()
                ;

            return ret;
        }
    }
}
