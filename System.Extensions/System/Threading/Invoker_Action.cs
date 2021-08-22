using System.Threading.Tasks;

namespace System.Threading
{
    public record Invoker_Action : Invoker
    {
        public Action<CancellationToken> Action { get; init; } = x => throw new NotImplementedException();

        public Task InvokeAsync(CancellationToken token = default)
        {
            return FuncInvokeAsync(ThreadName, ApartmentState, Action, token);
        }

        public Thread InvokeThread(CancellationToken Token = default)
        {
            return ThreadInvoke(ThreadName, ApartmentState, Action, Token);
        }

    }

}
