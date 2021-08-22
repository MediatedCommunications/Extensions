using System.Threading.Tasks;

namespace System.Threading
{
    public record Invoker_Func<T> : Invoker
    {
        public Func<CancellationToken, T> Action { get; init; } = x => throw new NotImplementedException();

        public Task<T> InvokeAsync(CancellationToken token = default)
        {
            return FuncInvokeAsync(ThreadName, ApartmentState, Action, token);
        }

    }

}
