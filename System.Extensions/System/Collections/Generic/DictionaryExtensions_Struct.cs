namespace System.Collections.Generic {

    public static class DictionaryExtensions_Struct {
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue>? This, TKey? Key, TValue Default = default)
            where TValue : struct {
            var ret = Default;

            if (This is { } && Key is { } V1 && This.TryGetValue(V1, out var V2)) {
                ret = V2;
            }

            return ret;
        }

    }

}
