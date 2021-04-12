namespace System.Threading.Threads {
    public static class ThreadInvoke {

        public static T Sta<T>(Func<T> M) {
            return Method(M, ApartmentState.STA);
        }

        public static T Mta<T>(Func<T> M) {
            return Method(M, ApartmentState.MTA);
        }

        public static T Method<T>(Func<T> M, ApartmentState A) {
            var ret = default(T);
            var Thread = DoInvoke(null, A, () => {
                ret = M();
            });

            Thread.Join();

            return ret!;
        }


        public static void Sta(Action A) {
            Sta(null, A);
        }

        public static void Sta(string? ThreadName, Action A) {
            StaThread(ThreadName, A).Join();
        }

        public static Thread StaThread(Action A) {
            return StaThread(null, A);
        }
        public static Thread StaThread(string? ThreadName, Action A) {
            return DoInvoke(ThreadName, ApartmentState.STA, A);
        }

        public static void Mta(Action A) {
            Mta(null, A);
        }

        public static void Mta(string? ThreadName, Action A) {
            MtaThread(ThreadName, A).Join();
        }
        public static Thread MtaThread(Action A) {
            return MtaThread(null, A);
        }

        public static Thread MtaThread(string? ThreadName, Action A) {
            return DoInvoke(ThreadName, ApartmentState.MTA, A);
        }


        private static Thread DoInvoke(string? ThreadName, ApartmentState ApartmentState, Action A) {

            if (ThreadName.IsBlank()) {
                var ST = new Diagnostics.StackTrace();
                ThreadName = ST.ToString();
            }

            var T = new Thread(() => {
                Thread.CurrentThread.Name = ThreadName;

                A();
            });

            if (OperatingSystem.IsWindows()) {
                T.SetApartmentState(ApartmentState);
            }

            T.Start();

            return T;
        }

    }
}
