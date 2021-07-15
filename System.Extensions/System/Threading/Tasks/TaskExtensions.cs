using System.Collections.Generic;
using System.Linq;

namespace System.Threading.Tasks {
    public static class TaskExtensions {

        public static List<Func<Task?>?> AsThreadedTasks(this IEnumerable<Func<Task?>?>? This) {
            var ret = new List<Func<Task?>?>();

            static async Task TaskRunner(Func<Task?> ChildTask) {
                await Task.Run(async () => {
                    var Child = ChildTask();
                    if (Child is { }) {
                        await Child
                        .DefaultAwait()
                        ;
                    }
                });
            }

            foreach (var item in This.Coalesce().WhereIsNotNull()) {
                ret.Add(() => TaskRunner(item));
            }

            return ret;

        }

        public static Task WhenAllAsync(this IEnumerable<Action?>? This, CancellationToken Token = default) {
            var Tasks = (
                from x in This.Coalesce().WhereIsNotNull()
                let tret = Task.Run(x)
                select tret
                ).ToLinkedList();

            return Tasks.WhenAllAsync(Token);
        }

        public static Task WhenAllAsync(this IEnumerable<Func<Task?>?>? This, CancellationToken Token = default) {
            var Tasks = new List<Task?>();

            foreach (var item in This.Coalesce()) {
                var Task = item?.Invoke();
                Tasks.Add(Task);
            }

            return Tasks.WhenAllAsync(Token);

        }


        public static async Task WhenAllAsync(this IEnumerable<Task?>? This, CancellationToken Token = default) {


            if (Token == default) {
                var Tasks = This.Coalesce().WhereIsNotNull().ToLinkedList();
                var MultiTask = Task.WhenAll(Tasks);
                await MultiTask
                    .DefaultAwait()
                    ;

            } else {

                var AggregateToken = CancellationTokenSource.CreateLinkedTokenSource(Token);
                var DelayTask = SafeDelay.DelayAsync(AggregateToken.Token);

                var Tasks = This.Coalesce().WhereIsNotNull().ToLinkedList();
                var MultiTask = Task.WhenAll(Tasks);

                var CompletedTask = await Task.WhenAny(
                    DelayTask,
                    MultiTask
                    ).DefaultAwait();
                
                CompletedTask.Ignore();

                AggregateToken.Cancel();

                
            }
        }

        public static async Task<Task> WhenAnyAsync(this IEnumerable<Task?> This, CancellationToken Token = default) {
            var AggregateToken = default(CancellationTokenSource?);

            var Tasks = new LinkedList<Task> {
                This.WhereIsNotNull(),
            };

            if(Token != default) {
                AggregateToken = CancellationTokenSource.CreateLinkedTokenSource(Token);
                Tasks.Add(SafeDelay.DelayAsync(AggregateToken.Token));
            }


            var CompletedTask = await Task.WhenAny(Tasks)
                .DefaultAwait()
                ;

            AggregateToken?.Cancel();

            return CompletedTask;
        }

        public static async Task<Task<T>?> WhenAnyAsync<T>(this IEnumerable<Task<T>> This, CancellationToken Token = default) {
            var AggregateToken = default(CancellationTokenSource?);

            var Tasks = new LinkedList<Task> {
                This,
            };

            if (Token != default) {
                AggregateToken = CancellationTokenSource.CreateLinkedTokenSource(Token);
                Tasks.Add(SafeDelay.DelayAsync(AggregateToken.Token));
            }

            var CompletedTask = await Task.WhenAny(Tasks)
                .DefaultAwait()
                ;

            AggregateToken?.Cancel();

            var ret = CompletedTask as Task<T>;

            return ret;
        }


    }
}
