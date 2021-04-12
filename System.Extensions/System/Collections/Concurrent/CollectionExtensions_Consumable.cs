using System.Collections.Generic;

namespace System.Collections.Concurrent {
    public static class CollectionExtensions_Consumable {
        public static IEnumerable<T> GetConsumingEnumerable<T>(this ConcurrentStack<T>? This) {
            if (This is { } V1) {
                while (This.TryPop(out var tret)) {
                    yield return tret;
                }
            }
        }

        public static IEnumerable<T> GetConsumingEnumerable<T>(this ConcurrentQueue<T>? This) {
            if (This is { } V1) {
                while (This.TryDequeue(out var tret)) {
                    yield return tret;
                }
            }
        }

        public static IEnumerable<T> GetConsumingEnumerable<T>(this ConcurrentBag<T>? This) {
            if (This is { } V1) {
                while (This.TryTake(out var tret)) {
                    yield return tret;
                }
            }
        }
    }
}
