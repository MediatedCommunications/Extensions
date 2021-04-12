namespace System.Collections.Generic {

    public static class DictionaryExtensions_Class {
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue>? This, TKey? Key, TValue Default)
            where TValue : class {
            var ret = Default;

            if (This is { } && Key is { } V1 && This.TryGetValue(V1, out var V2)) {
                ret = V2;
            }

            return ret;
        }

    }

}
