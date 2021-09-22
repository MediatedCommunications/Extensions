using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    public static class TimeSpans
    {
        public static TimeSpan MinValue { get; } = TimeSpan.MinValue;
        public static TimeSpan MaxValue { get; } = TimeSpan.MaxValue;

        public static TimeSpan Infinite { get; } = Timeout.InfiniteTimeSpan;

        public static TimeSpan Zero { get; } = TimeSpan.FromSeconds(0);
        public static TimeSpan OneSecond { get; } = TimeSpan.FromSeconds(1);
        public static TimeSpan OneMinute { get; } = TimeSpan.FromMinutes(1);
        public static TimeSpan OneHour { get; } = TimeSpan.FromHours(1);
    }

}
