namespace System.Collections.Generic {
    public static class DictionaryExtensions_Class_Class {
        public static TValue? TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue>? This, TKey? Key, TValue? Default = default)
            where TKey : class
            where TValue : class {
            var ret = Default;

            if (This is { } && Key is { } V1 && This.TryGetValue(V1, out var V2)) {
                ret = V2;
            }

            return ret;
        }

        public static TValue? TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue>? This, IEnumerable<TKey?> Keys, TValue? Default = default)
            where TKey : class
            where TValue : class {
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
