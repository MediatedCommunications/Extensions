using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Collections.Generic {

    public static class EnumerableExtensions {

        public static IEnumerable<T> Append<T>(this IEnumerable<T> This, params T[] Values) {
            return This.Append(Values.AsEnumerable());
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> This, params IEnumerable<T>[] Values) {

            foreach (var item in This) {
                yield return item;
            }

            foreach (var Group in Values) {
                foreach (var Item in Group) {
                    yield return Item;
                }
            }

        }


        public static IEnumerable<T> GetRange<T>(this IEnumerable<T>? source, Range Values) {
            var IE = source.EmptyIfNull();
            var Count = IE.Count();
            var Start = Values.Start.GetOffset(Count);
            var Finish = Values.End.GetOffset(Count);

            var Length = Finish - Start;
            if(Length < 0) {
                Length = 0;
            }

            var ret = IE.Skip(Start).Take(Length);

            return ret;
        }


        public static IEnumerable<WithIndexItem<T>> WithIndexes<T>(this IEnumerable<T>? source) {
            var Index = -1L;
            foreach (var Item in source.EmptyIfNull()) {
                Index += 1;
                var ret = WithIndexItem.Create(Index, Item);

                yield return ret;
            }
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source) {
            var ret = Enumerable.Empty<T>();
            
            if(source is { } V1) {
                ret = V1;
            }

            return ret;
        }

        public static async IAsyncEnumerable<T> EmptyIfNull<T>(this IAsyncEnumerable<T>? source) {
           
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

        public static async Task ConvertAsync<TInput>(this IAsyncEnumerable<TInput> This, Func<TInput, Task> Convert, Func<long>? Proposed_Concurrency = default, Func<TimeSpan?>? Evaluate_Concurrency = default) {

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


        public static async IAsyncEnumerable<TOutput> ConvertAsync<TInput, TOutput>(this IAsyncEnumerable<TInput> This, Func<TInput, Task<TOutput>> Convert, Func<long>? Proposed_Concurrency = default, Func<TimeSpan?>? Evaluate_Concurrency = default) {
            long Concurrency_Min() => 1;
            long Concurrency_Max() => 32;
            long Concurrency_Default() => 1;

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

            await foreach (var item in This.EmptyIfNull().DefaultAwait()) {

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

            await foreach (var item in This.EmptyIfNull().DefaultAwait()) {
                yield return item;

                if (CancelWhen()) {
                    yield break;
                }

            }
        }

        public static IAsyncEnumerable<T> WithGracefulCancellation<T>(this IAsyncEnumerable<T>? This, CancellationToken Token) {
            return This.WithGracefulCancellation(() => Token.ShouldStop());
        }

        public static IEnumerable<LinkedList<T>> BatchEvery<T>(this IEnumerable<T>? This, int Count = 1000) {
            if(Count <= 0) {
                Count = 1;
            }

            var ret = new LinkedList<T>();

            foreach (var item in This.EmptyIfNull()) {
                ret.Add(item);

                if(ret.Count >= Count) {
                    yield return ret;
                    ret = new LinkedList<T>();
                }
            }

            if (ret.Count > 0) {
                yield return ret;
            }
        }

        public static async IAsyncEnumerable<LinkedList<T>> BatchEvery<T>(this IAsyncEnumerable<T>? This, int Count = 1000) {
            if (Count <= 0) {
                Count = 1;
            }

            var ret = new LinkedList<T>();

            await foreach (var item in This.EmptyIfNull().DefaultAwait()) {
                ret.Add(item);

                if (ret.Count >= Count) {
                    yield return ret;
                    ret = new LinkedList<T>();
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

            await foreach (var item in This.EmptyIfNull().DefaultAwait()) {
                await Action(item, Count)
                    .DefaultAwait()
                    ;
                Count += 1;
            }
        }

    }

}
