namespace System {
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


    public enum DatePart {
        Unknown,
        Offset,
        MilliSecond,
        Second,
        Minute,
        Hour,
        Day,
        Month,
        Year,
        Universe,
    }

    public static class DateTimeOffsetExtensions {

        public static DateTimeOffset WithYear(this DateTimeOffset This, int Value) {
            var ret = new DateTimeOffset(Value, This.Month, This.Day, This.Hour, This.Minute, This.Second, This.Millisecond, This.Offset);

            return ret;
        }

        public static DateTimeOffset WithMonth(this DateTimeOffset This, int Value) {
            var ret = new DateTimeOffset(This.Year, Value, This.Day, This.Hour, This.Minute, This.Second, This.Millisecond, This.Offset);

            return ret;
        }


        public static DateTimeOffset WithDay(this DateTimeOffset This, int Value) {
            var ret = new DateTimeOffset(This.Year, This.Month, Value, This.Hour, This.Minute, This.Second, This.Millisecond, This.Offset);

            return ret;
        }


        public static DateTimeOffset WithHour(this DateTimeOffset This, int Value) {
            var ret = new DateTimeOffset(This.Year, This.Month, This.Day, Value, This.Minute, This.Second, This.Millisecond, This.Offset);

            return ret;
        }


        public static DateTimeOffset WithMinute(this DateTimeOffset This, int Value) {
            var ret = new DateTimeOffset(This.Year, This.Month, This.Day, This.Hour, Value, This.Second, This.Millisecond, This.Offset);

            return ret;
        }


        public static DateTimeOffset WithSecond(this DateTimeOffset This, int Value) {
            var ret = new DateTimeOffset(This.Year, This.Month, This.Day, This.Hour, This.Minute, Value, This.Millisecond, This.Offset);

            return ret;
        }


        public static DateTimeOffset WithMilisecond(this DateTimeOffset This, int Value) {
            var ret = new DateTimeOffset(This.Year, This.Month, This.Day, This.Hour, This.Minute, This.Second, Value, This.Offset);

            return ret;
        }

        public static DateTimeOffset WithOffset(this DateTimeOffset This, TimeSpan Value) {
            var ret = new DateTimeOffset(This.Year, This.Month, This.Day, This.Hour, This.Minute, This.Second, This.Millisecond, Value);

            return ret;
        }

        public static DateTimeOffset WithDate(this DateTimeOffset This, DateOnly Value) {
            var ret = new DateTimeOffset(Value.Year, Value.Month, Value.Day, This.Hour, This.Minute, This.Second, This.Millisecond, This.Offset);

            return ret;
        }

        public static DateTimeOffset WithDate(this DateTimeOffset This, DateTime Value) {
            var ret = new DateTimeOffset(Value.Year, Value.Month, Value.Day, This.Hour, This.Minute, This.Second, This.Millisecond, This.Offset);

            return ret;
        }

        public static DateTimeOffset WithDateTime(this DateTimeOffset This, DateTime Value) {
            var ret = new DateTimeOffset(Value.Year, Value.Month, Value.Day, Value.Hour, Value.Minute, Value.Second, Value.Millisecond, This.Offset);

            return ret;
        }



        public static DateTimeOffset StartOfYear(this DateTimeOffset This) {
            return This.StartOf(DatePart.Year);
        }

        public static DateTimeOffset StartOfMonth(this DateTimeOffset This) {
            return This.StartOf(DatePart.Month);
        }

        public static DateTimeOffset StartOf(this DateTimeOffset This, DatePart Part) {
            var Parts = new List<DatePart>();
            if(Part is DatePart.Universe or DatePart.Year or DatePart.Month or DatePart.Day or DatePart.Hour or DatePart.Minute or DatePart.Second) {
                Parts.Add(DatePart.MilliSecond);
            }

            if (Part is DatePart.Universe or DatePart.Year or DatePart.Month or DatePart.Day or DatePart.Hour or DatePart.Minute) {
                Parts.Add(DatePart.Second);
            }

            if (Part is DatePart.Universe or DatePart.Year or DatePart.Month or DatePart.Day or DatePart.Hour) {
                Parts.Add(DatePart.Minute);
            }

            if (Part is DatePart.Universe or DatePart.Year or DatePart.Month or DatePart.Day) {
                Parts.Add(DatePart.Hour);
            }

            if (Part is DatePart.Universe or DatePart.Year or DatePart.Month) {
                Parts.Add(DatePart.Day);
            }

            if (Part is DatePart.Universe or DatePart.Year) {
                Parts.Add(DatePart.Month);
            }

            if (Part is DatePart.Universe) {
                Parts.Add(DatePart.Year);
            }


            var ret = This.Min(Parts);

            return ret;
        }

        public static DateTimeOffset Min(this DateTimeOffset This, params DatePart[] Parts) {
            return This.Min(Parts.AsEnumerable());
        }

        public static DateTimeOffset Min(this DateTimeOffset This, IEnumerable<DatePart> Parts) {
            var ret = This;

            foreach (var Part in Parts) {
                ret = ret.Min(Part);
            }

            return ret;
        }

        public static DateTimeOffset Min(this DateTimeOffset This, DatePart Part) {

            var Year = This.Year;
            var Month = This.Month;
            var Day = This.Day;
            var Hour = This.Hour;
            var Minute = This.Minute;
            var Second = This.Second;
            var MilliSecond = This.Millisecond;
            var Offset = This.Offset;

            if (Part is DatePart.Universe) {
                return DateTimeOffset.MinValue;
            } else {
                if (Part is DatePart.Year) {
                    Year = 1;
                } else if (Part is DatePart.Month) {
                    Month = 1;
                } else if (Part is DatePart.Day) {
                    Day = 1;
                } else if (Part is DatePart.Hour) {
                    Hour = 0;
                } else if (Part is DatePart.Minute) {
                    Minute = 0;
                } else if (Part is DatePart.Second) {
                    Second = 0;
                } else if (Part is DatePart.MilliSecond) {
                    MilliSecond = 0;
                }

                var ret = new DateTimeOffset(Year, Month, Day, Hour, Minute, Second, MilliSecond, Offset);

                return ret;
            }
        }

    }
}
