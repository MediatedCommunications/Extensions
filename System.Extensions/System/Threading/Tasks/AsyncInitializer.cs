using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace System.Threading.Tasks {

    public abstract class AsyncInitializer<TContext> : AsyncInitializer {
        protected TContext Options { get; }

        public AsyncInitializer(TContext Context, params AsyncInitializer[] AsyncInitializers) : base(AsyncInitializers) {
            this.Options = Context;
        }
    }

    public abstract class AsyncInitializer {

        protected ImmutableArray<AsyncInitializer> AsyncInitializers { get; }

        public AsyncInitializer(params AsyncInitializer[] AsyncInitializers) {
            this.AsyncInitializers = AsyncInitializers.ToImmutableArray();
        }


        protected volatile Task? InitializationTask;

        protected async Task InitializeAsync() {
            await AsyncInitializers.WaitForInitializationAsync()
                .DefaultAwait()
                ;

            await InitializeInternalAsync()
                .DefaultAwait()
                ;

            //We do this to null out any extra resources associated with the original task
            InitializationTask = Task.CompletedTask;
        }

        protected abstract Task InitializeInternalAsync();

        public async Task WaitForInitializationAsync(CancellationToken Token = default) {

            lock (this) {
                if (InitializationTask is null) {
                    InitializationTask = Task.Run(() => InitializeAsync());
                }
            }

            var Tasks = new[] {
                InitializationTask
            };

            var Completed = await Tasks.WhenAnyAsync(Token)
                .DefaultAwait()
                ;

            Completed.Ignore();
        }

    }

    public static class AsyncInitializerExtensions {
        public static async Task WaitForInitializationAsync(this IEnumerable<AsyncInitializer> AsyncInitializers, CancellationToken Token = default) {
            var Tasks = new List<Task>();

            foreach (var AsyncInitializer in AsyncInitializers) {
                var NewTask = Task.Run(() => AsyncInitializer.WaitForInitializationAsync(Token));
                Tasks.Add(NewTask);
            }

            await Tasks.WhenAllAsync(Token)
                .DefaultAwait()
                ;
        }
    }

}

