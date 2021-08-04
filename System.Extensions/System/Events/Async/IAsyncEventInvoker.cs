using System.Threading.Tasks;

namespace System.Events.Async {

    public interface IAsyncEventInvoker<TSender, TData> {
        Task InvokeAsync(TSender sender, TData data);
    }
}
