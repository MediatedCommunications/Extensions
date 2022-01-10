namespace System.Threading {
    internal class ThreadPoolCompletionThreads : ThreadPoolThreads {
        public override int Min {
            get {
                ThreadPool.GetMinThreads(out _, out var completion);
                return completion;
            }
            set {
                ThreadPool.GetMinThreads(out var worker, out _);
                ThreadPool.SetMinThreads(worker, value);
            }
        }

        public override int Max {
            get {
                ThreadPool.GetMaxThreads(out _, out var completion);
                return completion;
            }
            set {
                ThreadPool.GetMaxThreads(out var worker, out _);
                ThreadPool.SetMaxThreads(worker, value);
            }
        }

        public override int Available {
            get {
                ThreadPool.GetAvailableThreads(out _, out var completion);

                return completion;
            }
        }
    }

}
