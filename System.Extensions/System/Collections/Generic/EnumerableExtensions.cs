using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Collections.Generic {



    public static class EnumerableExtensions {

        public static IEnumerable<T> Item<T>(this IEnumerable<T> This, int Index) {
            return This.Skip(Index).Take(1);
        }

        public static IEnumerable<T> Item<T>(this IEnumerable<T> This, Index Index) {
            if (Index.IsFromEnd) {
                return This.TakeLast(Index.Value).Take(1);
            }
            return This.Skip(Index.Value).Take(1);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> This)
        {
            return !This.Any();
        }

        public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> This) {
            return This.SelectMany(x => x);
        }

        public static async IAsyncEnumerable<T> SelectMany<T>(this IAsyncEnumerable<IEnumerable<T>> This) {
            await foreach (var Parent in This) {
                foreach (var Child in Parent) {
                    yield return Child;
                }
            }
        }

        public static async IAsyncEnumerable<T> SelectMany<T>(this IEnumerable<IAsyncEnumerable<T>> This) {
            foreach (var Parent in This) {
                await foreach(var Child in Parent.DefaultAwait()) {
                    yield return Child;
                }
            }
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> This, params T[] Values) {
            return This.Concat(Values.AsEnumerable());
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> This, params IEnumerable<T>[] Values) {
            var NewValues = new List<IEnumerable<T>>() {
                This,
                Values
            };

            return Concat(NewValues);

        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<IEnumerable<T>> This) {
            var ret = Enumerable.Empty<T>();

            foreach (var item in This) {
                if(item == null) {
                    throw new NullReferenceException();
                }

                if(ret == Enumerable.Empty<T>()) {
                    ret = item;
                } else {
                    ret = Enumerable.Concat(ret, item);
                }

            }

            return ret;
        }

        public static T[] TakeRange<T>(this IEnumerable<T>? source, Range Range) {
            var tret = source.EmptyIfNull().ToArray();
            var (Offset, Length) = Range.GetOffsetAndLength(tret.Length);

            var Start = Offset;
            Start = Math.Clamp(Start, 0, Start);

            var End = Offset + Length;
            End = Math.Clamp(End, 0, tret.Length);

            var ret = tret[Start..End];

            return ret;
        }


        public static IEnumerable<WithIndexItem<T>> WithIndexes<T>(this IEnumerable<T>? source, int FirstIndex = 0) {
            var Index = FirstIndex;
            foreach (var Item in source.EmptyIfNull()) {
                var ret = WithIndexItem.Create(Index, Item);
                yield return ret;

                Index += 1;
            }
        }

        public static async IAsyncEnumerable<WithIndexItem<T>> WithIndexes<T>(this IAsyncEnumerable<T>? source, int FirstIndex = 0) {
            var Index = FirstIndex;

            await foreach (var Item in source.EmptyIfNull().DefaultAwait()) {
                var ret = WithIndexItem.Create(Index, Item);
                yield return ret;

                Index += 1;
            }
        }


        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source) {
            return Coalesce(source, Enumerable.Empty<T>());
        }
        public static T? NullIfEmpty<T, U>(this T? source) where T : IEnumerable<U> {
            var ret = default(T?);
            if (source is { }) {
                var count = source.Count();

                if (count > 0) {
                    ret = source;
                }
            }

            return ret;
        }

        public static IEnumerable<T> Coalesce<T>(this IEnumerable<T>? source, params T[] Values) {
            return Coalesce(source, Values.AsEnumerable());
        }

        public static IEnumerable<T> Coalesce<T>(this IEnumerable<T>? source, params IEnumerable<T>?[] Values) {
            var ret = source is { } V1
                ? V1
                : Values.CoalesceInternal()
                ;

            return ret;

        }

        private static IEnumerable<T> CoalesceInternal<T>(this IEnumerable<IEnumerable<T>?>? This) {
            var ret = default(IEnumerable<T>?);

            if (This is { } V1) {
                foreach (var item in V1) {
                    if (item is { } V2) {
                        ret = V2;
                        break;
                    }
                }
            }

            ret ??= Enumerable.Empty<T>();

            return ret;
        }

        public static IAsyncEnumerable<T> EmptyIfNull<T>(this IAsyncEnumerable<T>? source) {
            return Coalesce(source, AsyncEnumerable.Empty<T>());
        }

        public static IAsyncEnumerable<T> Coalesce<T>(this IAsyncEnumerable<T>? source, params T[] Values) {
            return Coalesce(source, Values.AsAsyncEnumerable());
        }


        public static IAsyncEnumerable<T> Coalesce<T>(this IAsyncEnumerable<T>? source, params IAsyncEnumerable<T>?[] Values) {
            var ret = source is { } V1
                ? V1
                : Values.CoalesceInternal()
                ;

            return ret;

        }

        private static IAsyncEnumerable<T> CoalesceInternal<T>(this IEnumerable<IAsyncEnumerable<T>?>? This) {
            var ret = default(IAsyncEnumerable<T>?);

            if (This is { } V1) {
                foreach (var item in V1) {
                    if (item is { } V2) {
                        ret = V2;
                        break;
                    }
                }
            }

            ret ??= AsyncEnumerable.Empty<T>();

            return ret;
        }

        public static async IAsyncEnumerable<T> Coalesce<T>(this IAsyncEnumerable<T>? source) {
           
            if(source is { }) {
                await foreach (var item in source.DefaultAwait()) {
                    yield return item;
                }
            }
        }


        public static void ForEach<T>(this IEnumerable<T>? This, Action<T> Action) {
            foreach (var item in This.EmptyIfNull()) {
                Action(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T>? This, Action<T, int> Action) {
            var index = -1;
            
            foreach (var item in This.EmptyIfNull()) {
                index += 1;
                Action(item, index);
            }
        }

        public static IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IEnumerable<T> This) {
            return This.AsAsyncEnumerable(x => Task.FromResult(x));
        }

        public static async IAsyncEnumerable<TOutput> AsAsyncEnumerable<TInput, TOutput>(this IEnumerable<TInput> This, Func<TInput, Task<TOutput>> Convert) {

            foreach (var item in This.EmptyIfNull()) {
                var Converted = await Convert(item)
                    .DefaultAwait()
                    ;

                yield return Converted;
            }

        }

        public static IAsyncEnumerable<TInput> MergeAsync<TInput>(this IEnumerable<IAsyncEnumerable<TInput>> This, Func<int>? Proposed_Concurrency = default, Func<TimeSpan?>? Evaluate_Concurrency = default) {
            return This.AsAsyncEnumerable().MergeAsync(Proposed_Concurrency, Evaluate_Concurrency);
        }

        public static IAsyncEnumerable<TInput> MergeAsync<TInput>(this IAsyncEnumerable<IAsyncEnumerable<TInput>> This, Func<int>? Proposed_Concurrency = default, Func<TimeSpan?>? Evaluate_Concurrency = default) {
            return MergeAsync_Default(This, Proposed_Concurrency, Evaluate_Concurrency);
        }

        public static async IAsyncEnumerable<TInput> MergeAsync_Channel<TInput>(this IAsyncEnumerable<IAsyncEnumerable<TInput>> This, Func<int>? Proposed_Concurrency = default, Func<TimeSpan?>? Evaluate_Concurrency = default) {
            int Concurrency_Min() => 1;
            int Concurrency_Max() => 32;
            int Concurrency_Default() => 1;
            var Concurrency = Proposed_Concurrency ?? Concurrency_Default;

            Task DelayTask_Create() {
                var Duration = Evaluate_Concurrency?.Invoke() ?? TimeSpan.FromSeconds(1);

                return SafeDelay.DelayAsync(Duration);
            }

            Task DelayTask_Create_Async() {
                return Task.Run(() => DelayTask_Create());
            }

            var C = System.Threading.Channels.Channel.CreateUnbounded<TInput>(new Threading.Channels.UnboundedChannelOptions() {
                SingleReader = true,
                AllowSynchronousContinuations = false,
            });

            var Status = new ConcurrentDictionary<IAsyncEnumerable<TInput>, long>();

            async Task FillAsync(IAsyncEnumerable<TInput> Source) {
                var Count = 0;
                Status[Source] = Count;

                await foreach (var item in Source.DefaultAwait()) {
                    await C.Writer.WriteAsync(item)
                        .DefaultAwait()
                        ;
                    Count += 1;
                    Status[Source] = Count;
                }

                Count *= -1;
                Status[Source] = Count;
            }

            var Filler = Task.Run(async () => {
                var DelayTask = default(Task?);

                var Tasks = new List<Task>();

                var Source = This.GetAsyncEnumerator();
                var SourceEnded = false;

                while (!SourceEnded || Tasks.Count > 0) {

                    //Initialize our items
                    if (!SourceEnded) {
                        var Threads = Math.Clamp(Concurrency(), Concurrency_Min(), Concurrency_Max());

                        while (!SourceEnded && Tasks.Count < Threads) {
                            SourceEnded = !await Source.MoveNextAsync()
                                .DefaultAwait()
                                ;

                            if (!SourceEnded) {
                                var IE = Source.Current;
                                Tasks.Add(Task.Run(()=>FillAsync(IE)));
                            } else {

                            }

                        }
                    }

                    var CompletedItems = (
                        from x in Tasks
                        where x.IsCompleted
                        select x
                        ).ToList();

                    foreach (var item in CompletedItems) {
                        await item
                            .DefaultAwait()
                            ;
                        Tasks.Remove(item);
                    }

                    if (!SourceEnded) {
                        if(DelayTask is null || !Tasks.Contains(DelayTask)){
                            DelayTask = DelayTask_Create_Async();
                            Tasks.Add(DelayTask);
                        }
                    }

                    if (Tasks.Count > 0) {

                        var CompletedTask = await Task.WhenAny(Tasks)
                            .DefaultAwait()
                            ;

                    }

                }



                C.Writer.Complete();
            
            });

            await foreach (var item in C.Reader.ReadAllAsync().DefaultAwait()) {
                yield return item;
            }


        }

        public static async IAsyncEnumerable<TInput> MergeAsync_Default<TInput>(this IAsyncEnumerable<IAsyncEnumerable<TInput>> This, Func<int>? Proposed_Concurrency = default, Func<TimeSpan?>? Evaluate_Concurrency = default) {
            int Concurrency_Min() => 1;
            int Concurrency_Max() => 32;
            int Concurrency_Default() => 1;
            var Concurrency = Proposed_Concurrency ?? Concurrency_Default;

            Task DelayTask_Create() {
                var Duration = Evaluate_Concurrency?.Invoke() ?? TimeSpan.FromSeconds(1);

                return SafeDelay.DelayAsync(Duration);
            }

            Task DelayTask_Create_Async() {
                return Task.Run(() => DelayTask_Create());
            }

            var DelayTask = default(Task?);

            var Children = new Dictionary<IAsyncEnumerator<TInput>, Task<bool>?>();

            var Source = This.GetAsyncEnumerator();
            var SourceEnded = false;
            while (!SourceEnded || Children.Count > 0) {

                //Initialize our items
                if (!SourceEnded) {
                    var Threads = Math.Clamp(Concurrency(), Concurrency_Min(), Concurrency_Max());

                    while (!SourceEnded && Children.Count < Threads) {
                        SourceEnded = !await Source.MoveNextAsync()
                            .DefaultAwait()
                            ;

                        if (!SourceEnded) {
                            var IE = Source.Current.GetAsyncEnumerator();
                            Children[IE] = default;
                        } else {

                        }

                    }
                }

                var ItemsToAdvance = (
                    from x in Children
                    where x.Value?.IsCompleted ?? true
                    select x
                    ).ToList();

                var ValuesToReturn = new List<TInput>();

                foreach (var item in ItemsToAdvance) {

                    var ShouldAdvance = true;

                    if (item.Value is { } V1) {
                        var tret = await V1
                            .DefaultAwait()
                            ;

                        if (tret) {
                            ValuesToReturn.Add(item.Key.Current);
                        } else {
                            ShouldAdvance = false;
                        }
                    }
                    var ItemKey = item.Key;

                    if (ShouldAdvance) {
  

                        Children[ItemKey] = Task.Run(async () => {
                            var tret = false;
                            try {
                                tret = await ItemKey.MoveNextAsync()
                                    .DefaultAwait()
                                    ;
                            } catch (Exception ex) {
                                ex.Ignore();
                            }

                            return tret;
                        });
                    } else {
                        Children.Remove(ItemKey);
                    }
                }

                foreach (var item in ValuesToReturn) {
                    yield return item;
                }

                var Tasks = (from x in Children select (Task)x.Value).ToList();
                if (!SourceEnded) {
                    DelayTask ??= DelayTask_Create_Async();
                    Tasks.Add(DelayTask);
                }

                if (Tasks.Count > 0) {

                    var CompletedTask = await Task.WhenAny(Tasks)
                        .DefaultAwait()
                        ;

                    if (CompletedTask == DelayTask && !SourceEnded) {
                        DelayTask = DelayTask_Create_Async();
                    }

                }

            }

        }

        public static async Task ConvertAsync<TInput>(this IAsyncEnumerable<TInput> This, Func<TInput, Task> Convert, Func<int>? Proposed_Concurrency = default, Func<TimeSpan?>? Evaluate_Concurrency = default) {

            async Task<bool> NewConvert(TInput Input) {
                await Convert(Input)
                    .DefaultAwait()
                    ;
                return true;
            }

            await This.ConvertAsync(NewConvert, Proposed_Concurrency, Evaluate_Concurrency)
                .ForEachAsync()
                ;


        }


        public static async IAsyncEnumerable<TOutput> ConvertAsync<TInput, TOutput>(this IAsyncEnumerable<TInput> This, Func<TInput, Task<TOutput>> Convert, Func<int>? Proposed_Concurrency = default, Func<TimeSpan?>? Evaluate_Concurrency = default) {
            int Concurrency_Min() => 1;
            int Concurrency_Max() => 32;
            int Concurrency_Default() => 1;

            Task DelayTask_Create() {
                var Duration = Evaluate_Concurrency?.Invoke() ?? TimeSpan.FromSeconds(1);

                return SafeDelay.DelayAsync(Duration);
            }

            Task DelayTask_Create_Async() {
                return Task.Run(() => DelayTask_Create());
            }


            var Concurrency = Proposed_Concurrency ?? Concurrency_Default;

            var AllTasks = new List<Task?>();
            var ResultTasks = new List<Task?>();

            var DelayTask = DelayTask_Create_Async();
            AllTasks.Add(DelayTask);

            await foreach (var item in This.Coalesce().DefaultAwait()) {

                var NewTask = Task.Run(() => Convert(item));
                AllTasks.Add(NewTask);
                ResultTasks.Add(NewTask);

                var Threads = Math.Clamp(Concurrency(), Concurrency_Min(), Concurrency_Max());

                while (ResultTasks.Count >= Threads) {

                    var CompletedTask = await AllTasks.WhenAnyAsync()
                        .DefaultAwait()
                        ;

                    if(CompletedTask is { }) {
                        AllTasks.Remove(CompletedTask);
                        ResultTasks.Remove(CompletedTask);

                        if (CompletedTask == DelayTask) {
                            DelayTask = DelayTask_Create_Async();
                            AllTasks.Add(DelayTask);
                            
                        } else if (CompletedTask is Task<TOutput> OutputTask) {
                            var result = await OutputTask
                                .DefaultAwait()
                                ;
                            yield return result;
                        }
                    }

                }
            }

            AllTasks.Remove(DelayTask);

            while(AllTasks.Count > 0) {
                var CompletedTask = await AllTasks.WhenAnyAsync()
                        .DefaultAwait()
                        ;

                if (CompletedTask is { }) {
                    AllTasks.Remove(CompletedTask);
                    if (CompletedTask is Task<TOutput> OutputTask) {
                        var result = await OutputTask
                            .DefaultAwait()
                            ;
                        yield return result;
                    }
                }
            }
            

        }

        public static IEnumerable<T> WithGracefulCancellation<T>(this IEnumerable<T>? This, Func<bool> CancelWhen) {
            if (CancelWhen()) {
                yield break;
            }

            foreach (var item in This.EmptyIfNull()) {
                yield return item;

                if (CancelWhen()) {
                    yield break;
                }

            }
        }

        public static IEnumerable<T> WithGracefulCancellation<T>(this IEnumerable<T>? This, CancellationToken Token) {
            return This.WithGracefulCancellation(() => Token.ShouldStop());
        }

        public static async IAsyncEnumerable<T> WithGracefulCancellation<T>(this IAsyncEnumerable<T>? This, Func<bool> CancelWhen) {
            if (CancelWhen()) {
                yield break;
            }

            await foreach (var item in This.Coalesce().DefaultAwait()) {
                yield return item;

                if (CancelWhen()) {
                    yield break;
                }

            }
        }

        public static IAsyncEnumerable<T> WithGracefulCancellation<T>(this IAsyncEnumerable<T>? This, CancellationToken Token) {
            return This.WithGracefulCancellation(() => Token.ShouldStop());
        }

        public static IEnumerable<T> Partition<T>(this IEnumerable<T>? This, int Partitions = 2) {
            if(Partitions <= 0) {
                throw new ArgumentOutOfRangeException(nameof(Partitions));
            }

            var Queues = new List<Queue<T>>();
            while(Queues.Count < Partitions) {
                Queues.Add(new Queue<T>());
            }

            foreach (var (Index, Item) in This.EmptyIfNull().WithIndexes()) {
                var Bucket = (int)(Index % Queues.Count);

                Queues[Bucket].Enqueue(Item);
            }

            foreach (var Queue in Queues) {
                while(Queue.TryDequeue(out var Item)) {
                    yield return Item;
                }
            }



        }

        public static IAsyncEnumerable<LinkedList<T>> Chunk<T>(this IAsyncEnumerable<T>? This, AdaptiveChunker Chunker) {
            return Chunker.Chunk(This.Coalesce());
        }

        public static async IAsyncEnumerable<LinkedList<T>> Chunk<T>(this IAsyncEnumerable<T>? This, long Count = 1000) {
            if (Count <= 0) {
                Count = 1;
            }

            var ret = new LinkedList<T>();

            await foreach (var item in This.Coalesce().DefaultAwait()) {
                ret.Add(item);

                if (ret.Count >= Count) {
                    yield return ret;
                    ret = new();
                }
            }

            if (ret.Count > 0) {
                yield return ret;
            }

        }

        public static Task ForEachAsync<T>(this IAsyncEnumerable<T>? This) {
            return ForEachAsync(This, (x, y) => Task.CompletedTask);
        }

        public static Task ForEachAsync<T>(this IAsyncEnumerable<T>? This, Action<T> Action) {
            return ForEachAsync(This, (x, y) => Action(x));
        }

        public static Task ForEachAsync<T>(this IAsyncEnumerable<T>? This, Action<T, long> Action) {
            return ForEachAsync(This, (x, y) => {
                Action(x, y);
                return Task.CompletedTask;
            });
        }

        public static Task ForEachAsync<T>(this IAsyncEnumerable<T>? This, Func<T, Task> Action) {
            return ForEachAsync(This, (x, y) => Action(x));
        }

        public static async Task ForEachAsync<T>(this IAsyncEnumerable<T>? This, Func<T, long, Task> Action) {
            var Count = 0;

            await foreach (var item in This.Coalesce().DefaultAwait()) {
                await Action(item, Count)
                    .DefaultAwait()
                    ;
                Count += 1;
            }
        }

    }

}
