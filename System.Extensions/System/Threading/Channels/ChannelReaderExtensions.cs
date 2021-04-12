using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading.Channels {
    public static class ChannelReaderExtensions {

        public static async IAsyncEnumerable<T> WithGracefulCancellation<T>(this ChannelReader<T> This, [EnumeratorCancellation] CancellationToken Token = default) {

            var IE = This.ReadAllAsync(Token).GetAsyncEnumerator(Token);
            var Continue = true;
            while (Continue) {
                try {
                    Continue = await IE.MoveNextAsync()
                        .DefaultAwait()
                        ;
                } catch (OperationCanceledException ex) {
                    ex.Ignore();
                    Continue = false;
                }

                if (Continue) {
                    yield return IE.Current;
                }

            }

        }

    }
}
