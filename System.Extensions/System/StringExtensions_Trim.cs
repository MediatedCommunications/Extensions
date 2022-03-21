using System.Collections.Generic;

namespace System
{
    public static class StringExtensions_Trim {
        public static IEnumerable<string> Trim(this IEnumerable<string?>? This) {
            foreach (var item in This.EmptyIfNull()) {
                var ret = item.Coalesce().Trim();

                yield return ret;
            }
        }

    }

}
