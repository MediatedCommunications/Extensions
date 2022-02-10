using System.Collections.Immutable;
using System.Data;
using System.Linq;

namespace System.Collections.Generic
{
    public static class CollectionExtensions_Add {

        public static ImmutableList<T> Add<T>(this ImmutableList<T> This, params T[] Group) {
            return This.Add(new[] { Group });
        }

        public static ImmutableList<T> Add<T>(this ImmutableList<T> This, params IEnumerable<T>?[]? Values) {

            static ImmutableList<T> Adder(ImmutableList<T> Collection, IEnumerable<T> Items) {
                return Collection.AddRange(Items);
            }

            return This.Add(Values, Adder);
        }


        public static void Add<T>(this ICollection<T> This, params T[] Group) {
            This.Add(Group.AsEnumerable());
        }

        public static void Add<T>(this ICollection<T> This, params IEnumerable<T>?[]? Values) {
            
            static ICollection<T> Adder(ICollection<T> Collection, IEnumerable<T> Items) {
                foreach (var Item in Items) {
                    Collection.Add(Item);
                }

                return Collection;
            }

            This.Add(Values, Adder);

        }

        public static void Set<TKey, TValue>(this IDictionary<TKey, TValue> This, IEnumerable<KeyValuePair<TKey, TValue>>? Items) {
            This.Add(Items, true);
        }

        public static void Set<TKey, TValue>(this IDictionary<TKey, TValue> This, IEnumerable<TValue>? Items)
            where TValue : IIdResult<TKey> {
            This.Add(Items, true);
        }

        public static void Set<TKey, TValue>(this IDictionary<TKey, TValue> This, IEnumerable<TValue>? Items, Func<TValue, TKey> KeySelector) {
            This.Add(Items, KeySelector, x => x, true);
        }

        public static void Set<TKey, TValue, TItem>(this IDictionary<TKey, TValue> This, IEnumerable<TItem>? Items, Func<TItem, TKey> KeySelector, Func<TItem, TValue> ValueSelector) {
            This.Add(Items, KeySelector, ValueSelector, true);
        }

        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> This, IEnumerable<KeyValuePair<TKey, TValue>>? Items, bool Set = false) {
            This.Add(Items, x => x.Key, x => x.Value, true);
        }

        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> This, IEnumerable<TValue>? Items, bool Set = false) 
            where TValue : IIdResult<TKey>
            {
            This.Add(Items, x => x.Id, x => x, Set);
        }

        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> This, IEnumerable<TValue>? Items, Func<TValue, TKey> KeySelector, bool Set = false) {
            This.Add(Items, KeySelector, x => x, Set);
        }

        public static void Add<TKey, TValue, TItem>(this IDictionary<TKey, TValue> This, IEnumerable<TItem>? Items, Func<TItem, TKey> KeySelector, Func<TItem, TValue> ValueSelector, bool Set = false) {
            IDictionary<TKey, TValue> Adder(IDictionary<TKey, TValue> Target, IEnumerable<KeyValuePair<TKey, TValue>> Items) {
                var tret = Target;
                foreach (var Item in Items) {
                    if (Set) {
                        tret[Item.Key] = Item.Value;
                    } else {
                        tret.Add(Item);
                    }
                }
                return tret;
            }

            var Query = (
                from x in Items
                let Key = KeySelector(x)
                let Value = ValueSelector(x)
                select KeyValuePair.Create(Key, Value)
                ).ToList();

            This.Add(new[] { Query }, Adder);

        }


        private static TCollection Add<TCollection, TItem>(this TCollection This, IEnumerable<TItem>?[]? Values, Func<TCollection, IEnumerable<TItem>, TCollection> Adder) {
            var ret = This;

            var ToAdd = (
                from x in Values.EmptyIfNull()
                from y in x.EmptyIfNull()
                select y
                ).ToList();

            ret = Adder(ret, ToAdd);

            return ret;
        }

    }

}
