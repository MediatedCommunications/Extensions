using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic {
    public static partial class EnumerableExtensions {

        //This makes it work for Immutable Arrays
        public static IEnumerable<T> EmptyIfNull<T>(this ImmutableArray<T>? source) {

            return Coalesce(source, Enumerable.Empty<T>());
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source) {
            return Coalesce(source, Enumerable.Empty<T>());
        }
    }
}
