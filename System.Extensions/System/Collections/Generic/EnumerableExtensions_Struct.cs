using System.Linq;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions_Struct {
        
        public static IEnumerable<T> WhereIsNotNull<T>(this IEnumerable<T?>? This) where T : struct {
            return This.EmptyIfNull().OfType<T>();
        }

        public static IAsyncEnumerable<T> WhereIsNotNull<T>(this IAsyncEnumerable<T?>? This) where T : struct {
            return This.Coalesce().OfType();
        }

        public static async IAsyncEnumerable<T> OfType<T>(this IAsyncEnumerable<T?>? source) where T : struct {
            await foreach (var item in source.Coalesce().DefaultAwait()) {
                if (item is T { } V1) {
                    yield return V1;
                }
            }
        }

        public static T? FirstOrNull<T>(this IEnumerable<T>? This) where T : struct {
            var ret = default(T?);

            var tret = This.EmptyIfNull().Take(1).ToList();

            if(tret.Count > 0) {
                ret = tret[0];
            }

            return ret;
        }

        public static IEnumerable<T?> NullIfEmpty<T>(this IEnumerable<T>? This) where T:struct {
            var tret = This.EmptyIfNull().ToList();

            if(tret.Count == 0) {
                yield return null;
            } else {
                foreach (var item in tret) {
                    yield return item;
                }
            }

        } 

    }

}
