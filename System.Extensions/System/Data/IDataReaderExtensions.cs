using System.Collections.Immutable;
using System.Linq;

namespace System.Data
{
    public static class IDataReaderExtensions {

        public static ImmutableHashSet<string> GetColumnNames(this IDataReader This) {
            var ret = (
                from x in Enumerable.Range(0, This.FieldCount)
                let Name = This.GetName(x)
                select Name
                ).ToImmutableHashSet(StringComparer.InvariantCultureIgnoreCase);

            return ret;
        }
        
    }
}
