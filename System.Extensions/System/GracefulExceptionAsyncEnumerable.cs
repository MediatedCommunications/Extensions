using System.Diagnostics;

namespace System {
    public record GracefulExceptionAsyncEnumerable<T> : DisplayRecord, IAsyncEnumerable<T> {
        public IAsyncEnumerable<T> Source { get; init; } = AsyncEnumerable.Empty<T>();
        public Func<Exception, Task>? OnError { get; init; }
        
        public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) {
            var IE = Source.GetAsyncEnumerator(cancellationToken);

            var Continue = true;
            while (Continue) {
                var MoveNext = false;
                var Error = default(Exception?);

                try {
                    MoveNext = await IE.MoveNextAsync()
                        .DefaultAwait()
                        ;
                } catch (Exception ex) {
                    Error = ex;
                }

                if (MoveNext) {
                    yield return IE.Current;
                } else if (Error is { } && OnError is { } ErrorHandler) {
                    try {
                        await ErrorHandler(Error)
                            .DefaultAwait()
                            ;
                    } catch (Exception exx) {
                        exx.Ignore();
                    }

                }

                Continue = MoveNext;
            }
            

        }
    }
}
