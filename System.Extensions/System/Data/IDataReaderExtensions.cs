using System.Collections.Immutable;
using System.Linq;

namespace System.Data
{
    public static class IDataReaderExtensions {

        public static ImmutableList<string> GetColumnNames(this IDataReader This) {
            var ret = (
                from x in Enumerable.Range(0, This.FieldCount)
                let Name = This.GetName(x)
                select Name
                ).ToImmutableList();

            return ret;
        }
        
    }
}
