namespace System {
    public static class Math2 {

        public static int Ceiling(int Value, int Nearest) {
            var V = Value / (double)Nearest;

            return (int)(Math.Ceiling(V) * Nearest);
        }

        public static int Floor(int Value, int Nearest) {
            var V = Value / (double)Nearest;

            return (int)(Math.Floor(V) * Nearest);
        }

        public static long Ceiling(long Value, long Nearest) {
            var V = Value / (double)Nearest;

            return (long)(Math.Ceiling(V) * Nearest);
        }

        public static long Floor(long Value, long Nearest) {
            var V = Value / (double)Nearest;

            return (long)(Math.Floor(V) * Nearest);
        }



        public static DateTime Floor(this DateTime This, TimeSpan Span) {
            var ret = This;
            if (Span.Ticks > 0) {
                var Ticks = This.Ticks / Span.Ticks;
                ret = new DateTime(Ticks * Span.Ticks);
            }

            return ret;
        }

        public static DateTime Round(this DateTime This, TimeSpan Span) {
            var ret = This;
            if (Span.Ticks > 0) {
                var Ticks = (This.Ticks + (This.Ticks / 2) + 1) / This.Ticks;
                ret = new DateTime(Ticks * Span.Ticks);
            }

            return ret;
        }

        public static DateTime Ceiling(this DateTime This, TimeSpan Span) {
            var ret = This;

            if (Span.Ticks > 0) {
                var Ticks = (This.Ticks + Span.Ticks - 1) / Span.Ticks;
                ret = new DateTime(Ticks * Span.Ticks);
            }

            return ret;
        }


    }
}
