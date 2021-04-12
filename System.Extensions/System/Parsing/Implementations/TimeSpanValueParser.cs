namespace System {
    public record TimeSpanValueParser : StructParser<TimeSpan> {
        public TimeSpanFormat Format { get; init; }

        public TimeSpanValueParser(TimeSpanFormat Format, string? Value) : base(Value) {
            this.Format = Format;
        }

        public override bool TryGetValue(out TimeSpan Result) {
            var ret = false;
            Result = TimeSpan.Zero;

            if(Format == TimeSpanFormat.TimeSpan) {
                ret = TimeSpan.TryParse(Value, out Result);
            } else if(Format is TimeSpanFormat.Days or TimeSpanFormat.Hours or TimeSpanFormat.Minutes or TimeSpanFormat.Seconds or TimeSpanFormat.Milliseconds) {
                if (Value.Parse().AsDouble().TryGetValue(out var Double)) {
                    ret = true;
                    Result = Format switch {
                        TimeSpanFormat.Days => TimeSpan.FromDays(Double),
                        TimeSpanFormat.Hours => TimeSpan.FromHours(Double),
                        TimeSpanFormat.Minutes => TimeSpan.FromMinutes(Double),
                        TimeSpanFormat.Seconds => TimeSpan.FromSeconds(Double),
                        _ => TimeSpan.FromMilliseconds(Double),
                    };
                }
            } else if(Format == TimeSpanFormat.Ticks) {
                if(Value.Parse().AsLong().TryGetValue(out var Long)) {
                    ret = true;
                    Result = TimeSpan.FromTicks(Long);
                }
            }

            return ret;
        }
    }

}
