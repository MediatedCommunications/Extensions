using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading.Tasks.Dataflow {
    public static class DataFlowExtensions {

        public static Task FillAsync<T>(this ITargetBlock<T> Destination, IAsyncEnumerable<T> Source, bool? NewThread = default, bool? Complete = default, CancellationToken Token = default) {
            var NewThreadValue = NewThread ?? true;
            var CompleteValue = Complete ?? true;

            var ret = NewThreadValue
                ? Task.Run(()=> FillInternalAsync(Source, Destination, CompleteValue, Token))
                : FillInternalAsync(Source, Destination, CompleteValue, Token)
                ;

            return ret;

        }

        private static async Task FillInternalAsync<T>(IAsyncEnumerable<T> Source, ITargetBlock<T> Destination, bool Complete, CancellationToken Token = default) {
            await foreach (var item in Source.WithGracefulCancellation(Token).DefaultAwait()) {

                var tret = await Destination.SendAsync(item, Token)
                    .DefaultAwait()
                    ;

                if (tret == false) {
                    break;
                }

            }

            if (Complete) {
                Destination.Complete();
            }

        }

        public static Task FillAsync<T>(this ITargetBlock<T> Destination, IEnumerable<T> Source, bool? NewThread = default, bool? Complete = default, CancellationToken Token = default) {
            var NewThreadValue = NewThread ?? true;
            var CompleteValue = Complete ?? true;

            var ret = FillInternalAsync(Source, Destination, CompleteValue, Token);

            if (NewThreadValue) {
                ret = Task.Run(() => ret);
            }

            return ret;

        }

        private static async Task FillInternalAsync<T>(IEnumerable<T> Source, ITargetBlock<T> Destination, bool Complete, CancellationToken Token = default) {
            foreach (var item in Source.WithGracefulCancellation(Token)) {

                var tret = await Destination.SendAsync(item, Token)
                    .DefaultAwait()
                    ;

                if (tret == false) {
                    break;
                }

            }

            if (Complete) {
                Destination.Complete();
            }

        }
    }

   
}
    
