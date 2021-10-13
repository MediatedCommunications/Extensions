using System.Threading.Tasks;

namespace System.Net.Http.Message
{
    public interface IHttpRequestMessageBuilder {
        Task ModifyAsync(HttpRequestMessage Message);
    }
}
