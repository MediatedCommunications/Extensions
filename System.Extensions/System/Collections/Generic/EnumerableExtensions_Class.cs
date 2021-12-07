using System.Linq;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions_Class {

        public static IEnumerable<T> WhereIsNotNull<T>(this IEnumerable<T?>? This) where T : class {
            return This.EmptyIfNull().OfType<T>();
        }

        public static IAsyncEnumerable<T> WhereIsNotNull<T>(this IAsyncEnumerable<T?>? This) where T : class {
            return This.EmptyIfNull().OfType<T>();
        }

    }

}
