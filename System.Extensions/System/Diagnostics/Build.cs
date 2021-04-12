namespace System.Diagnostics {
    public static partial class Build {
        public const string DEBUG = "DEBUG";
        public const string RELEASE = "RELEASE";

        public static string Current {
            get {
                var ret = IsRelease
                    ? RELEASE
                    : DEBUG
                    ;

                return ret;
            }
        }



        public static bool IsDebug {
            get {
                var ret = false;
                SetInDebug(ref ret, true);
                return ret;
            }
        }

        [Conditional(DEBUG)]
        public static void SetInDebug<T>(ref T Target, T Value) {
            Target = Value;
        }


        public static bool IsRelease {
            get {
                var ret = false;
                SetInRelease(ref ret, true);
                return ret;
            }
        }

        [Conditional(RELEASE)]
        public static void SetInRelease<T>(ref T Target, T Value) {
            Target = Value;
        }

    }
}
