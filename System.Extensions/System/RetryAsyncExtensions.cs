using System.Threading;
using System.Threading.Tasks;

namespace System
{
    public static class RetryAsyncExtensions
    {
        public static RetryAsync<T> WithAction<T>(this RetryAsync<T> This, Func<CancellationToken, Task<T>> Value)
        {
            return This with
            {
                Try = Value,
            };
        }

        public static RetryAsync<T> WithDefault<T>(this RetryAsync<T> This, Func<CancellationToken, Task<T>> Value)
        {
            return This with
            {
                Default = Value,
            };
        }

        
    }


}
