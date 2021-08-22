using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message
{
    public interface IMessageSender<TRequest, TResponse> {
        Task<TResponse> SendAsync(TRequest Message, CancellationToken Token);
    }

}
