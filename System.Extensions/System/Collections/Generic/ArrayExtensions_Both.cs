namespace System.Collections.Generic {
    public static class ArrayExtensions_Both {
        public static TValue? TryGetValue<TValue>(this TValue[]? This, int? Key, TValue? Default = default) {
            var ret = Default;

            if (This is { } V1 && Key is { } V2 && V2 >= 0 && V2 < V1.Length) {
                ret = V1[V2];
            }

            return ret;
        }
    }
}
