using System.Collections.Generic;

namespace System.Collections.Concurrent {
    public static class EnumerableExtensions_ToCollections {

        public static ConcurrentStack<T> ToConcurrentStack<T>(this IEnumerable<T>? This) {
            var ret = new ConcurrentStack<T>(This.Coalesce());

            return ret;
        }

        public static ConcurrentQueue<T> ToConcurrentQueue<T>(this IEnumerable<T>? This) {
            var ret = new ConcurrentQueue<T>(This.Coalesce());

            return ret;
        }
    }

}
