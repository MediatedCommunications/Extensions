using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Threading.Tasks {

    public enum ConcurrentWhenCanceled {
        AbandonChildTasks,
        WaitForChildTasks,
        ThrowException,
    }

    /// <summary>
    /// Concurrently runs the specified Tasks until either one or all of the tasks complete.
    /// If no tasks are provided, nothing happens.
    /// </summary>
    public record Concurrent {
        public ImmutableList<TimeSpan> Timeouts { get; init; } = ImmutableList<TimeSpan>.Empty;
        public ImmutableList<CancellationToken> Tokens { get; init; } = ImmutableList<CancellationToken>.Empty;
        public ImmutableList<Func<CancellationToken, Task>> Tasks { get; init; } = ImmutableList<Func<CancellationToken, Task>>.Empty;

        public ConcurrentWhenCanceled WhenCanceled { get; init; } = ConcurrentWhenCanceled.AbandonChildTasks;

        private bool StartTasks(
            [NotNullWhen(true)] out CancellationTokenSource? MasterToken, 
            [NotNullWhen(true)] out List<Task>? AllTasks, 
            [NotNullWhen(true)] out List<Task>? RunningTasks,
            [NotNullWhen(true)] out List<Task>? DelayTasks
            ) {
            var ret = false;

            MasterToken = default;
            AllTasks = default;
            RunningTasks = default;
            DelayTasks = default;

            if (Tasks.Count > 0) {
                var CompletionTokens = new LinkedList<CancellationToken>() {
                    Tokens,
                };

                if(CompletionTokens.Count == 0) {
                    CompletionTokens.Add(CancellationToken.None);
                }

                var MMasterToken = CancellationTokenSource.CreateLinkedTokenSource(CompletionTokens.ToArray());
                MasterToken = MMasterToken;

                if (Timeouts.Count > 0) {
                    var SmallestDelay = (from x in Timeouts orderby x ascending select x).First();
                    MasterToken.CancelAfter(SmallestDelay);
                }

                AllTasks = new List<Task>();

                RunningTasks = new List<Task>();
                foreach (var TaskCreator in Tasks) {
                    var ThisTask = Task.Run(() => TaskCreator(MMasterToken.Token));
                    RunningTasks.Add(ThisTask);
                    AllTasks.Add(ThisTask);
                }

                DelayTasks = new List<Task>();
                {
                    var ThisTask = SafeDelay.DelayAsync(MasterToken.Token);
                    DelayTasks.Add(ThisTask);
                    AllTasks.Add(ThisTask);
                }

                ret = true;
            }

            return ret;
        }

        public async Task WhenAllAsync() {
            if (StartTasks(out var MasterToken, out var AllTasks, out var RunningTasks, out var DelayTasks)) {

                var Canceled = false;

                while (!Canceled && RunningTasks.Count > 0) {
                    var CompletedTask = await Task.WhenAny(AllTasks)
                    .DefaultAwait()
                    ;

                    if (DelayTasks.Contains(CompletedTask)) {
                        Canceled = true;
                        MasterToken.Cancel();
                        await DoCancellationAsync(AllTasks, RunningTasks, DelayTasks, CompletedTask)
                            .DefaultAwait()
                            ;

                    } else {
                        AllTasks.Remove(CompletedTask);
                        RunningTasks.Remove(CompletedTask);
                    }

                    if (RunningTasks.Count == 0) {
                        MasterToken.Cancel();
                    }

                }
            }

           
        }

        public async Task WhenAnyAsync() {
            if (StartTasks(out var MasterToken, out var AllTasks, out var RunningTasks, out var DelayTasks)) {

                if (RunningTasks.Count > 0) {

                    var CompletedTask = await Task.WhenAny(AllTasks)
                        .DefaultAwait()
                        ;

                    MasterToken.Cancel();


                    if (DelayTasks.Contains(CompletedTask)) {

                        await DoCancellationAsync(AllTasks, RunningTasks, DelayTasks, CompletedTask)
                            .DefaultAwait()
                            ;
                    }
                }
            }
        }

        private async Task DoCancellationAsync(List<Task> AllTasks, List<Task> RunningTasks, List<Task> DelayTasks, Task CompletedTask) {

            if (WhenCanceled == ConcurrentWhenCanceled.AbandonChildTasks) {
                // Do Nothing
            } else if (WhenCanceled == ConcurrentWhenCanceled.ThrowException) {
                throw new TimeoutException();
            } else if (WhenCanceled == ConcurrentWhenCanceled.WaitForChildTasks) {
                await Task.WhenAll(RunningTasks)
                    .DefaultAwait()
                    ;
            }

        }

        public static Concurrent ForEach<T>(IEnumerable<T>? Items, Func<T, CancellationToken, Task> Action) {
            var ToDo = new List<Func<CancellationToken, Task>>();

            foreach (var item in Items.Coalesce()) {
                ToDo.Add(x => Action(item, x));
            }

            var ret = new Concurrent() {
                Tasks = ToDo.ToImmutableList(),
            };

            return ret;
        }

        public static Concurrent ForEach<T>(IEnumerable<T>? Items, Action<T, CancellationToken> Action) {
            Task NewAction(T Item, CancellationToken Token) {
                Action(Item, Token);
                return Task.CompletedTask;
            }

            return ForEach(Items, NewAction);

        }


    }

}
