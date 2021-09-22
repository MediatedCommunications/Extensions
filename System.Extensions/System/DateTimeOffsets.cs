namespace System
{
    public static class DateTimeOffsets
    {
        public static DateTimeOffset MinValue { get; } = DateTimeOffset.MinValue;
        public static DateTimeOffset MaxValue { get; } = DateTimeOffset.MaxValue;
        public static DateTimeOffset Unknown { get; } = new DateTimeOffset(1901, 01, 01, 0, 0, 0, TimeSpan.Zero);

        public static DateTimeOffset Now => DateTimeOffset.Now;
        public static DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }

}
