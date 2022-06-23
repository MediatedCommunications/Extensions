using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Immutable {
    public static class ImmutableDictionaryExtensions {
        
        public static ImmutableDictionary<TKey, TValue> SetItems<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue> This
            , params KeyValuePair<TKey, TValue>[] Items
            )
            where TKey : notnull
            {

            return This.SetItems(Items);

        }

        public static ImmutableDictionary<TKey, TValue> SetItems<TKey, TValue>(
            this ImmutableDictionary<TKey, TValue> This
            , params ValueTuple<TKey, TValue>[] Items
            )
            where TKey : notnull {

            return This.SetItems(Items.ToKeyValuePairs());

        }

    }
}
