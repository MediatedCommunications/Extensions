using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Threading;
using System.Runtime.CompilerServices;

namespace System.Linq {
    public static class AsyncEnumerableHelpers {
        public static async IAsyncEnumerable<T> Prefetch<T>(this IAsyncEnumerable<T> This, int Count = int.MaxValue, [EnumeratorCancellation] CancellationToken Token = default) {
            var Options = new BoundedChannelOptions(Count) {
                AllowSynchronousContinuations = false,
                FullMode = BoundedChannelFullMode.Wait,
                SingleReader = true,
                SingleWriter = true,
            };

            var C = Channel.CreateBounded<T>(Options);

            var Filler = Task.Run(async () => {
                try {
                    await foreach (var item in This.DefaultAwait().WithCancellation(Token)) {
                        await C.Writer.WriteAsync(item, Token)
                            .DefaultAwait()
                            ;
                    }
                    C.Writer.TryComplete();
                } catch (Exception ex) { 
                    C.Writer.TryComplete(ex);
                }
            });

            await foreach (var item in C.Reader.ReadAllAsync(Token).DefaultAwait()) {
                yield return item;
            }

        }
    
    }
}
