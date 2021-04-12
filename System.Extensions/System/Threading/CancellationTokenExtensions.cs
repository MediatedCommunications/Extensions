using System.Runtime.CompilerServices;

namespace System.Threading {
    public static class CancellationTokenExtensions {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShouldContinue(this CancellationToken? This) {
            return This?.IsCancellationRequested != true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShouldStop(this CancellationToken? This) {
            return This?.IsCancellationRequested == true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShouldContinue(this CancellationToken This) {
            return !This.IsCancellationRequested;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShouldStop(this CancellationToken This) {
            return This.IsCancellationRequested;
        }

    }
}
