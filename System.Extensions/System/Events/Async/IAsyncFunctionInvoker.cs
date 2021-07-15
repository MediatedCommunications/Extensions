using System.Threading.Tasks;

namespace System.Events.Async {
    public interface IAsyncFunctionInvoker<TSender, TArgs, TResult> {
        Task<TResult?> InvokeAsync(TSender sender, TArgs args);
    }
}
