using System.Diagnostics;
using System.Threading;

namespace System
{
    public abstract record RetryBase : DisplayRecord
    {
        public Func<int, TimeSpan> DelayBeforeRecover { get; init; } = x => TimeSpan.Zero;
        public Func<int, TimeSpan> DelayAfterRecover { get; init; } = x => TimeSpan.FromSeconds(x);

        public long MaxAttempts { get; init; } = 3;

        public RetryFailureResult OnFailure { get; init; } = RetryFailureResult.ReturnDefault;
        public CancellationToken Token { get; init; } = default;
    }

}
