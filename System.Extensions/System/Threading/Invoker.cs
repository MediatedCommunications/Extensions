using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Threading
{
    public record Invoker : DisplayRecord
    {
        public string ThreadName { get; init; } = string.Empty;
        public ApartmentState ApartmentState { get; init; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder)
        {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(ThreadName)
                .Postfix.Add(ApartmentState)
                ;
        }

        protected static Thread ThreadInvoke(string? ThreadName, ApartmentState ApartmentState, Action<CancellationToken> A, CancellationToken Token)
        {

            if (ThreadName.IsBlank())
            {
                var ST = new StackTrace();
                ThreadName = ST.ToString();
            }

            var T = new Thread(() => {
                Thread.CurrentThread.TrySetName(ThreadName);

                A(Token);
            });
            T.SetApartmentState(ApartmentState);
            T.Start();

            return T;
        }

        protected static Task FuncInvokeAsync(string? ThreadName, ApartmentState A, Action M)
        {
            return FuncInvokeAsync(ThreadName, A, x => M(), default);
        }

        protected static Task FuncInvokeAsync(string? ThreadName, ApartmentState A, Action<CancellationToken> M, CancellationToken Token = default) {
            return FuncInvokeAsync(ThreadName, A, x => { 
                M(Token); 
                return true; 
            }, Token);
        }

        protected static Task<T> FuncInvokeAsync<T>(string? ThreadName, ApartmentState A, Func<T> M) {
            return FuncInvokeAsync(ThreadName, A, x => M(), default);
        }

        protected static async Task<T> FuncInvokeAsync<T>(string? ThreadName, ApartmentState A, Func<CancellationToken, T> M, CancellationToken Token = default)
        {
            var tcs = TaskCompletionFactory.Create<T>();

            var Thread = ThreadInvoke(ThreadName, A, token => {
                
                try { 
                    var ret = M(Token);
                    tcs.SetResult(ret);
                } catch(Exception ex)
                {
                    tcs.SetException(ex);
                }

            }, Token);

            var ret = await tcs.Task
                .DefaultAwait()
                ;

            return ret;
        }


    }

}
