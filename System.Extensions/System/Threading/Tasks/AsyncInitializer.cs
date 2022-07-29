using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;

namespace System.Threading.Tasks {

    public abstract class DictionaryAsyncInitializer<TContext, TId, TData> : ListAsyncInitializer<TContext, TData> 
        where TId : notnull
        {

        public ImmutableDictionary<TId, TData> ById { get; private set; } = ImmutableDictionary<TId, TData>.Empty;

        protected DictionaryAsyncInitializer(TContext Context, params AsyncInitializer?[] AsyncInitializers) : base(Context, AsyncInitializers) {
        
        }

        protected virtual TId GetId(TData Input) {
            if(HasId.TryGetId<TId>(Input, out var V1) && V1 is { }) { 
                return V1;
            } else {
                throw new NotImplementedException();
            }
        }

        protected override async Task InitializeInternalAsync() {
            await base.InitializeInternalAsync()
                .DefaultAwait()
                ;

            this.ById = this.AsList
                .ToImmutableDictionary(x => GetId(x))
                ;

        }

    }

    public abstract class ListAsyncInitializer<TContext, TData> : AsyncInitializer<TContext> {

        public ImmutableArray<TData> AsList { get; private set; } = ImmutableArray<TData>.Empty;        

        protected ListAsyncInitializer(TContext Context, params AsyncInitializer?[] AsyncInitializers) : base(Context, AsyncInitializers) {
        
        }

        protected abstract IAsyncEnumerable<TData> ListItemsAsync();

        protected override async Task InitializeInternalAsync() {
            var tret = await ListItemsAsync()
                .ToListAsync()
                .DefaultAwait()
                ;

            this.AsList = tret.ToImmutableArray();
        }


    }

    public abstract class AsyncInitializer<TContext> : AsyncInitializer {
        protected TContext Options { get; }

        public AsyncInitializer(TContext Context, params AsyncInitializer?[] AsyncInitializers) : base(AsyncInitializers) {
            this.Options = Context;
        }
    }

    public abstract class AsyncInitializer {

        protected ImmutableArray<AsyncInitializer> AsyncInitializers { get; }

        public AsyncInitializer(params AsyncInitializer?[] AsyncInitializers) {
            this.AsyncInitializers = AsyncInitializers.WhereIsNotNull().ToImmutableArray();
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

