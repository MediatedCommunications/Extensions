using System.Collections.Immutable;

namespace System.Collections.Generic {
    public static class CollectionExtensions {

        public static ImmutableList<T> Add<T>(this ImmutableList<T> This, params T?[] Group) {
            return This.Add(new[] { Group });
        }

        public static ImmutableList<T> Add<T>(this ImmutableList<T> This, params IEnumerable<T?>[]? Values) {

            static ImmutableList<T> Adder(ImmutableList<T> Collection, T Item) {
                return Collection.Add(Item);
            }

            return This.Add(Values, Adder);
        }


        public static void Add<T>(this ICollection<T> This, params T?[] Group) {
            This.Add(new[] { Group });
        }

        public static void Add<T>(this ICollection<T> This, params IEnumerable<T?>[]? Values) {
            
            static ICollection<T> Adder(ICollection<T> Collection, T Item) {
                Collection.Add(Item);

                return Collection;
            }

            This.Add(Values, Adder);

        }

        private static TCollection Add<TCollection, TItem>(this TCollection This, IEnumerable<TItem?>[]? Values, Func<TCollection, TItem, TCollection> Adder) {
            var ret = This;
            foreach (var Group in Values.EmptyIfNull()) {
                foreach (var Item in Group.EmptyIfNull()) {

                    if (Item is { } V1) {
                        ret = Adder(ret, Item);
                    }


                }
            }

            return ret;
        }

    }

}
