namespace System
{
    public static class DateTimes
    {
        public static DateTime MinValue { get; } = DateTime.MinValue;
        public static DateTime MaxValue { get; } = DateTime.MaxValue;
        public static DateTime Unknown { get; } = new DateTime(1901, 01, 01, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime Now => DateTime.Now;
        public static DateTime UtcNow => DateTime.UtcNow;

        public static bool Match(DateTime V1, DateTime V2) {
            return Match(V1, V2, TimeSpans.OneSecond);
        }

        public static bool Match(DateTime V1, DateTime V2, TimeSpan Tolerance) {
            var ret = false;
            var Delta = V1 - V2;
            if (Delta >= -Tolerance && Delta <= Tolerance) {
                ret = true;
            }

            return ret;
        }
    }

}
