using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace System.Collections.Generic {
    public class AdaptiveChunker : AdaptiveChunkerBase {

        private static int[] GoodChunks() {
            var ret = new[] {
                1,
                10,
                50,
                100,
                250,
                500,
                1000
            };

            return ret;
        }

        public AdaptiveChunker(int? EveryCount = default, TimeSpan? EveryTime = default, IEnumerable<int>? Initial = default) {
            this.EveryCount = EveryCount ?? 1000;
            this.EveryTime = EveryTime ?? TimeSpans.OneMinute;
            this.Initial = (
                from x in (Initial ?? GoodChunks())
                where x < this.EveryCount
                select x
                ).ToImmutableHashSet();
        }

        protected ImmutableHashSet<int> Initial { get; }
        protected TimeSpan EveryTime { get; }
        protected int EveryCount { get; }

        protected override async IAsyncEnumerable<ChunkResult<T>> ChunkInternal<T>(IAsyncEnumerable<T> This) {
            var MyInitial = Initial;
            var MyInitialCounter = 0;

            var EveryCounter = 0;

            var IE = This.GetAsyncEnumerator();

            var ChunkStarted = DateTimeOffset.Now;
            var ResultTask = Task.Run(async() => await IE.MoveNextAsync().DefaultAwait());
            var WaitTask = SafeDelay.DelayAsync(EveryTime);

            while (true) {
                var Completion = await Task.WhenAny(WaitTask, ResultTask)
                    .DefaultAwait()
                    ;

                var StartNewChunk = false;

                if(Completion == ResultTask) {
                    var Success = await ResultTask
                        .DefaultAwait()
                        ;

                    if (Success) {
                        var ret = IE.Current;

                        yield return new ChunkResultItem<T>(ret);

                        ResultTask = Task.Run(async () => await IE.MoveNextAsync().DefaultAwait());

                        {
                            EveryCounter = (EveryCounter + 1) % EveryCount;

                            if (EveryCounter == 0) {
                                StartNewChunk = true;
                            }
                        }

                        {

                            if (MyInitial.Count > 0) {
                                MyInitialCounter += 1;

                                if (MyInitial.Contains(MyInitialCounter)) {
                                    MyInitial = MyInitial.Remove(MyInitialCounter);
                                    StartNewChunk = true;
                                }
                            }

                        }


                    } else {
                        break;
                    }
                } else if(Completion == WaitTask) {
                    var Elapsed = DateTimeOffset.Now - ChunkStarted;
                    var Remaining = EveryTime - Elapsed;
                    var NextDelay = Remaining <= TimeSpan.Zero
                        ? EveryTime
                        : Remaining
                        ;

                    if(Remaining <= TimeSpan.Zero) {
                        StartNewChunk = true;
                    }

                    WaitTask = SafeDelay.DelayAsync(NextDelay);


                }

                if (StartNewChunk) {
                    yield return new ChunkResultEnd<T>();
                    ChunkStarted = DateTimeOffset.Now;
                }

            }






        }
    }

}
