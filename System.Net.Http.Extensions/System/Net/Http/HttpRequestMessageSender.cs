using System.Diagnostics;
using System.Net.Http.Message;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public abstract record HttpRequestMessageSender : DisplayRecord, IHttpRequestMessageSender {
        public abstract Task<HttpResponseMessage> SendAsync(IHttpRequestMessageBuilder Message, CancellationToken Token);
    }

}
