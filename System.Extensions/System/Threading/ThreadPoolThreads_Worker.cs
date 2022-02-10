namespace System.Threading {
    internal class ThreadPoolThreads_Worker : ThreadPoolThreads {
        public override int Min { 
            get {
                ThreadPool.GetMinThreads(out var worker, out _);
                return worker;
            }
            set {
                ThreadPool.GetMinThreads(out _, out var completion);
                ThreadPool.SetMinThreads(value, completion);
            }
        }

        public override int Max {
            get {
                ThreadPool.GetMaxThreads(out var worker, out _);
                return worker;
            }
            set {
                ThreadPool.GetMaxThreads(out _, out var completion);
                ThreadPool.SetMaxThreads(value, completion);
            }
        }

        public override int Available {
            get {
                ThreadPool.GetAvailableThreads(out var worker, out _);

                return worker;
            }
        }


    }

}
