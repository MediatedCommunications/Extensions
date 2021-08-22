using System.Threading.Tasks;

namespace System.Net.Http.Message
{
    public interface IMessageModifier {
        Task ModifyAsync(HttpRequestMessage Message);
    }
}
