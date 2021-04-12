using System.Linq;

namespace System.Collections.Generic {
    public static class EnumerableExtensions_Struct {
        
        public static IEnumerable<T> WhereIsNotNull<T>(this IEnumerable<T?>? This) where T : struct {
            return This.EmptyIfNull().OfType<T>();
        }

        public static async IAsyncEnumerable<T> OfType<T>(this IAsyncEnumerable<T?>? source) where T : struct {
            await foreach (var item in source.EmptyIfNull().DefaultAwait()) {
                if (item is T { } V1) {
                    yield return V1;
                }
            }
        }

        public static IAsyncEnumerable<T> WhereIsNotNull<T>(this IAsyncEnumerable<T?>? This) where T : struct {
            return This.EmptyIfNull().OfType();
        }

    }

}
