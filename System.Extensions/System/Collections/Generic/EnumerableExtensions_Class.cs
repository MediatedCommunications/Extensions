using System.Linq;

namespace System.Collections.Generic {
    public static class EnumerableExtensions_Class {

        public static IEnumerable<T> WhereIsNotNull<T>(this IEnumerable<T?>? This) where T : class {
            return This.Coalesce().OfType<T>();
        }

        public static IAsyncEnumerable<T> WhereIsNotNull<T>(this IAsyncEnumerable<T?>? This) where T : class {
            return This.Coalesce().OfType<T>();
        }

        public static async IAsyncEnumerable<T> OfType<T>(this IAsyncEnumerable<object?>? source) where T : class {
            await foreach (var item in source.Coalesce().DefaultAwait()) {
                if (item is T { } V1) {
                    yield return V1;
                }
            }
        }



    }

}
