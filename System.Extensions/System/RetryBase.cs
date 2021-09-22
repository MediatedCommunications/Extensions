using System.Diagnostics;
using System.Threading;

namespace System
{
    public abstract record RetryBase : DisplayRecord
    {
        public TimeSpan DelayBeforeRecover { get; init; } = TimeSpan.Zero;
        public TimeSpan DelayAfterRecover { get; init; } = TimeSpan.FromSeconds(1);

        public long RetryAttempts { get; init; } = 3;

        public RetryFailureResult OnFailure { get; init; } = RetryFailureResult.ReturnDefault;
        public CancellationToken Token { get; init; } = default;
    }

}
