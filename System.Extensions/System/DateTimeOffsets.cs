namespace System
{
    public static class DateTimeOffsets
    {
        public static DateTimeOffset MinValue { get; } = DateTimeOffset.MinValue;
        public static DateTimeOffset MaxValue { get; } = DateTimeOffset.MaxValue;
        public static DateTimeOffset Unknown { get; } = new DateTimeOffset(1901, 01, 01, 0, 0, 0, TimeSpan.Zero);

        public static DateTimeOffset Now => DateTimeOffset.Now;
        public static DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

        public static bool Match(DateTimeOffset V1, DateTimeOffset V2) {
            return Match(V1, V2, TimeSpans.OneSecond);
        }

        public static bool Match(DateTimeOffset V1, DateTimeOffset V2, TimeSpan Tolerance) {
            var ret = false;
            var Delta = V1 - V2;
            if(Delta >= -Tolerance && Delta <= Tolerance) {
                ret = true;
            }

            return ret;
        }
    }

}
