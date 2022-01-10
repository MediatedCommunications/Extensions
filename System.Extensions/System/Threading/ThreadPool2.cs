using System.Diagnostics;

namespace System.Threading {
    public static class ThreadPool2 {

        public static ThreadPoolThreads WorkerThreads { get; } = new ThreadPoolWorkerThreads();
        public static ThreadPoolThreads CompletionThreads { get; } = new ThreadPoolCompletionThreads();

    }

}
