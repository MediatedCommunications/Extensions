namespace System.Collections.Generic {
    public static class DictionaryExtensions_Struct_Struct {
        public static TValue? TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue>? This, TKey? Key, TValue? Default = default)
            where TKey : struct
            where TValue : struct {
            var ret = Default;

            if (This is { } && Key is { } V1 && This.TryGetValue(V1, out var V2)) {
                ret = V2;
            }

            return ret;
        }

        public static TValue? TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue>? This, IEnumerable<TKey?> Keys, TValue? Default = default)
    where TKey : struct
    where TValue : struct {
            var ret = Default;

            foreach (var Key in Keys) {
                if (This is { } && Key is { } V1 && This.TryGetValue(V1, out var V2)) {
                    ret = V2;
                    break;
                }
            }

            return ret;
        }
    }

}
