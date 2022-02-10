using System.Diagnostics;

namespace System.Threading {
    public class ThreadPool2 : DisplayClass {
        public static ThreadPool2 Instance { get; }
        
        static ThreadPool2() {
            Instance = new ThreadPool2();
        }

        private ThreadPool2() {
            WorkerThreads = new ThreadPoolThreads_Worker();
            CompletionThreads = new ThreadPoolThreads_Completion();
        }

        public ThreadPoolThreads WorkerThreads { get; } 
        public ThreadPoolThreads CompletionThreads { get; } 

        public long Threads {
            get {
                return ThreadPool.ThreadCount;
            }
        }

        public long Pending {
            get {
                return ThreadPool.PendingWorkItemCount;
            }
        }

        public long Completed {
            get {
                return ThreadPool.CompletedWorkItemCount;
            }
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.AddCount(Threads)
                .Postfix.AddCount(Pending)
                .Postfix.AddCount(Completed)
                ;
        }

    }

}
