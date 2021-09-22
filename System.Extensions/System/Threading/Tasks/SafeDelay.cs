namespace System.Threading.Tasks {
    public static class SafeDelay {

        public static Task DelayAsync(CancellationToken Token) {
            return DelayAsync(TimeSpan.MaxValue, Token);
        }

        public static Task DelayAsync(double MsDuration, CancellationToken Token = default) {
            var Duration = TimeSpan.FromMilliseconds(MsDuration);

            return DelayAsync(Duration, Token);
        }

        public static async Task DelayAsync(TimeSpan Duration, CancellationToken Token = default) {

            try {
                if (Duration.TotalMilliseconds > int.MaxValue) {
                    Duration = Timeout.InfiniteTimeSpan;
                }

                if (Duration == Timeout.InfiniteTimeSpan || Duration > TimeSpan.Zero) {

                    await Task.Delay(Duration, Token)
                        .DefaultAwait()
                        ;
                }

            } catch (Exception ex) {
                ex.Ignore();
            }


        }

        public static void Delay(CancellationToken Token)
        {
            Delay(TimeSpan.MaxValue, Token);
        }

        public static void Delay(double MsDuration, CancellationToken Token = default)
        {
            var Duration = TimeSpan.FromMilliseconds(MsDuration);

            Delay(Duration, Token);
        }

        public static void Delay(TimeSpan Duration, CancellationToken Token = default)
        {

            try
            {
                
                var Start = DateTimeOffset.UtcNow;

                if (Duration.TotalMilliseconds > int.MaxValue)
                {
                    Duration = Timeout.InfiniteTimeSpan;
                }

                
                while(Token.ShouldContinue())
                {
                    var ToDelay = Duration == Timeout.InfiniteTimeSpan
                        ? TimeSpans.OneSecond
                        : DateTimeOffset.UtcNow - Start
                        ;

                    if(ToDelay > TimeSpans.OneSecond)
                    {
                        ToDelay = TimeSpans.OneSecond;
                    }

                    if(ToDelay > TimeSpans.Zero)
                    {
                        Thread.Sleep(ToDelay);
                    } else
                    {
                        break;
                    }

                }


            } catch (Exception ex)
            {
                ex.Ignore();
            }


        }

    }
}
