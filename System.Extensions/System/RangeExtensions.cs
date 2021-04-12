namespace System.Collections.Generic {
    public static class RangeExtensions {

        private static IEnumerable<int> AsEnumerableInternal(this Range This, int? LastValue = default, bool? Inclusive = default) {
            return This.AsEnumerable(LastValue ?? int.MaxValue, Inclusive ?? false);
        }
        
        private static IEnumerable<int> AsEnumerable(this Range This, int LastValue, bool Inclusive) {
            var Start = This.Start.GetOffset(LastValue);
            var Finish = This.End.GetOffset(LastValue);

            var Step = Start < Finish 
                ? 1
                : -1
                ;

            for (var i = Start; i != Finish; i += Step) {
                yield return i;
            }

            if (Inclusive) {
                yield return Finish;
            }

        }

        public static IEnumerable<int> AsEnumerable(this Range This) {
            return This.AsEnumerableInternal(default, default);
        }

        public static IEnumerable<int> AsEnumerable(this Range This, int LastValue) {
            return This.AsEnumerableInternal(LastValue, default);
        }

        public static IEnumerable<int> AsEnumerable(this Range This, bool Inclusive) {
            return This.AsEnumerableInternal(default, Inclusive);
        }

    }

}
