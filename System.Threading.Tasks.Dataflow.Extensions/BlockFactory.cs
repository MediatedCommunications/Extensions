using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace System.Threading.Tasks.Dataflow {
    public static class BlockFactory {

        public static DataflowLinkOptions LinkOptions(bool? PropagateCompletion = default, int? MaxMessages = default, bool? Append = default) {
            var ret = new DataflowLinkOptions()
            {
                PropagateCompletion = PropagateCompletion ?? true,
                MaxMessages = MaxMessages ?? DataflowBlockOptions.Unbounded,
                Append = Append ?? true,
                
            };

            return ret;
        }

        public static DataflowBlockOptions BlockOptions(CancellationToken Token = default) {
            var ret = new DataflowBlockOptions()
            {
                CancellationToken = Token,
                EnsureOrdered = false,
            };

            return ret;
        }

        public static ExecutionDataflowBlockOptions ExecutionOptions(int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            var ret = new ExecutionDataflowBlockOptions()
            {
                CancellationToken = Token,
                MaxDegreeOfParallelism = MaxDegreeOfParallelism ?? int.MaxValue,
                EnsureOrdered = false,
            };

            return ret;
        }

        public static GroupingDataflowBlockOptions GroupingOptions(CancellationToken Token = default) {
            var ret = new GroupingDataflowBlockOptions()
            {
                CancellationToken = Token,
                EnsureOrdered = false,
            };

            return ret;
        }



        public static DataflowBlockOptions BufferOptions(CancellationToken Token = default) {
            return BlockOptions(Token);
        }

        public static GroupingDataflowBlockOptions ChunkOptions(CancellationToken Token = default) {
            return GroupingOptions(Token);
        }

        public static ExecutionDataflowBlockOptions DoOptions(int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return ExecutionOptions(MaxDegreeOfParallelism, Token);
        }

        public static ExecutionDataflowBlockOptions SelectOptions(int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return ExecutionOptions(MaxDegreeOfParallelism, Token);
        }

        public static ExecutionDataflowBlockOptions SelectManyOptions(int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return ExecutionOptions(MaxDegreeOfParallelism, Token);
        }

        public static ExecutionDataflowBlockOptions WhereOptions(int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return ExecutionOptions(MaxDegreeOfParallelism, Token);
        }





        public static BufferBlock<T> Buffer<T>(CancellationToken Token = default) {
            return Buffer<T>(BufferOptions(Token));
        }


        public static BufferBlock<T> Buffer<T>(DataflowBlockOptions Options) {
            var ret = new BufferBlock<T>(Options);

            return ret;
        }

        public static BatchBlock<T> Chunk<T>(int Size, CancellationToken Token = default) {
            return Chunk<T>(Size, ChunkOptions(Token));
        }

        public static BatchBlock<T> Chunk<T>(int Size, GroupingDataflowBlockOptions Options) {
            var ret = new BatchBlock<T>(Size, Options);

            return ret;
        }




        public static ActionBlock<T> Do<T>(Action<T> Action, int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return Do(Action, DoOptions(MaxDegreeOfParallelism, Token));
        }

        public static ActionBlock<T> Do<T>(Action<T> Action, ExecutionDataflowBlockOptions Options) {
            var ret = new ActionBlock<T>(Action, Options);

            return ret;
        }

        public static ActionBlock<T> Do<T>(Func<T, Task> Action, int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return Do(Action, DoOptions(MaxDegreeOfParallelism, Token));
        }

        public static ActionBlock<T> Do<T>(Func<T, Task> Action, ExecutionDataflowBlockOptions Options) {
            var ret = new ActionBlock<T>(Action, Options);

            return ret;
        }

        public static ITargetBlock<T> Do<T>() {
            //return DataflowBlock.NullTarget<T>();
            var Options = DoOptions();

            return Do<T>(x => { }, Options);

        }



        public static TransformBlock<T, T> Select<T>(Action<T> Action, int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return Select(Action, SelectOptions(MaxDegreeOfParallelism, Token));
        }

        public static TransformBlock<T, T> Select<T>(Action<T> Action, ExecutionDataflowBlockOptions Options) {
            var ret = new TransformBlock<T, T>(x =>
            {
                Action(x);
                return x;
            }, Options);

            return ret;
        }

        public static TransformBlock<T, T> Select<T>(Func<T, Task> Action, int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return Select(Action, SelectOptions(MaxDegreeOfParallelism, Token));
        }

        public static TransformBlock<T, T> Select<T>(Func<T, Task> Action, ExecutionDataflowBlockOptions Options) {
            var ret = new TransformBlock<T, T>(async x =>
            {
                await Action(x)
                    .DefaultAwait()
                    ;

                return x;
            }, Options);

            return ret;
        }


        public static TransformBlock<T, U> Select<T, U>(Func<T, U> Action, int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return Select(Action, SelectOptions(MaxDegreeOfParallelism, Token));
        }

        public static TransformBlock<T, U> Select<T, U>(Func<T, U> Action, ExecutionDataflowBlockOptions Options) {
            var ret = new TransformBlock<T, U>(Action, Options);
            return ret;
        }


        public static TransformBlock<T, U> Select<T, U>(Func<T, Task<U>> Action, int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return Select(Action, SelectOptions(MaxDegreeOfParallelism, Token));
        }

        public static TransformBlock<T, U> Select<T, U>(Func<T, Task<U>> Action, ExecutionDataflowBlockOptions Options) {
            var ret = new TransformBlock<T, U>(Action, Options);
            return ret;
        }





        public static TransformManyBlock<IEnumerable<T>, T> SelectMany<T>(int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return SelectMany<T>(SelectManyOptions(MaxDegreeOfParallelism, Token));
        }

        public static TransformManyBlock<IEnumerable<T>, T> SelectMany<T>(ExecutionDataflowBlockOptions Options) {
            return SelectMany<IEnumerable<T>, T>(x => x, Options);
        }


        public static TransformManyBlock<T, U> SelectMany<T, U>(Func<T, IEnumerable<U>> Action, int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return SelectMany(Action, SelectManyOptions(MaxDegreeOfParallelism, Token));
        }

        public static TransformManyBlock<T, U> SelectMany<T, U>(Func<T, IEnumerable<U>> Action, ExecutionDataflowBlockOptions Options) {
            var ret = new TransformManyBlock<T, U>(Action, Options);
            return ret;
        }

        public static TransformManyBlock<T, U> SelectMany<T, U>(Func<T, Task<IEnumerable<U>>> Action, int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return SelectMany(Action, SelectManyOptions(MaxDegreeOfParallelism, Token));
        }

        public static TransformManyBlock<T, U> SelectMany<T, U>(Func<T, Task<IEnumerable<U>>> Action, ExecutionDataflowBlockOptions Options) {
            var ret = new TransformManyBlock<T, U>(Action, Options);
            return ret;
        }

        public static TransformManyBlock<T, U> SelectMany<T, U>(Func<T, IAsyncEnumerable<U>> Action, int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return SelectMany(Action, SelectManyOptions(MaxDegreeOfParallelism, Token));
        }

        public static TransformManyBlock<T, U> SelectMany<T, U>(Func<T, IAsyncEnumerable<U>> Action, ExecutionDataflowBlockOptions Options) {
            var ret = new TransformManyBlock<T, U>(async x =>
            {
                var v = await Action(x)
                    .ToListAsync()
                    .DefaultAwait()
                    ;

                return v;
            }, Options);
            return ret;
        }








        public static TransformManyBlock<T, T> Where<T>(Func<T, bool> Action, int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return Where(Action, WhereOptions(MaxDegreeOfParallelism, Token));
        }

        public static TransformManyBlock<T, T> Where<T>(Func<T, bool> Action, ExecutionDataflowBlockOptions Options) {
            var ret = new TransformManyBlock<T, T>(x =>
            {
                var Valid = Action(x);

                var tret = Valid
                    ? new[] { x }
                    : Array.Empty<T>();
                    ;

                return tret;
            }, Options);

            return ret;
        }

        public static TransformManyBlock<T, T> Where<T>(Func<T, Task<bool>> Action, int? MaxDegreeOfParallelism = default, CancellationToken Token = default) {
            return Where(Action, WhereOptions(MaxDegreeOfParallelism, Token));
        }

        public static TransformManyBlock<T, T> Where<T>(Func<T, Task<bool>> Action, ExecutionDataflowBlockOptions Options) {
            var ret = new TransformManyBlock<T, T>(async x =>
            {
                var Valid = await Action(x)
                    .DefaultAwait()
                    ;

                var tret = Valid
                    ? new[] { x }
                    : Array.Empty<T>();
                ;

                return tret;
            }, Options);

            return ret;
        }

    }
}
