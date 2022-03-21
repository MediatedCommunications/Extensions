using System.Collections.Generic;
using System.Linq;

namespace System {
    public static class IndexExtensions {

        private static bool TryGetOffset(this Index This, int ExclusiveMax, out int Offset) {
            return TryGetOffset(This, 0, ExclusiveMax, out Offset);
        }

            private static bool TryGetOffset(this Index This, int InclusiveMin, int ExclusiveMax, out int Offset) {
            var ret = false;

            Offset = This.GetOffset(ExclusiveMax);

            if (Offset >= InclusiveMin && Offset < ExclusiveMax) {
                ret = true;
            }

            return ret;
        }

        public static bool TryGetOffset<T>(this Index This, IEnumerable<T> Collection, out int Offset) {
            return TryGetOffset(This, Collection.Count(), out Offset);
        }

        public static bool TryGetOffset<T>(this IEnumerable<T> This, Index Index, out int Offset) {
            return TryGetOffset(Index, This, out Offset);
        }

        public static int GetOffset<T>(this IEnumerable<T> This, Index Index) {
            var Count = This.Count();
            return Index.GetOffset(Count);
        }

    }

}
