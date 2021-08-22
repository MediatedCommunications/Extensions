using System;
using System.Diagnostics.CodeAnalysis;

namespace CsvHelper
{
    public static class DynamicCsvRecordExtensions {

        public static string TryGetValue(this DynamicCsvRecord This, string Column) {
            This.TryGetValue(Column, out var tret);
            
            var ret = tret.Coalesce();

            return ret;
        }

        public static bool TryGetValue(this DynamicCsvRecord This, string Column, [NotNullWhen(true)] out string? Result) {
            var ret = false;
            Result = default;

            if(This.HeaderToIndex.TryGetValue(Column, out var Index) && Index < This.Values.Count) {
                ret = true;
                Result = This.Values[Index];
            }


            return ret;
        }

    }

}
