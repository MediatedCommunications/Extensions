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

    }
}
