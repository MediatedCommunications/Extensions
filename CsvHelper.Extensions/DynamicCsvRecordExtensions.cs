using System;
using System.Diagnostics.CodeAnalysis;

namespace CsvHelper
{
    public static class DynamicCsvRecordExtensions {



        public static string TryGetValue(this DynamicCsvRecord This, string? ColumnHeader) {
            This.TryGetValue(ColumnHeader, out var tret);
            
            var ret = tret.Coalesce();

            return ret;
        }

        public static bool TryGetValue(this DynamicCsvRecord This, string? ColumnHeader, [NotNullWhen(true)] out string? Result) {
            var ret = false;
            Result = default;

            if(ColumnHeader is { } V1 && This.HeaderToIndex.TryGetValue(V1, out var Index) && Index < This.Values.Length) {
                ret = true;
                Result = This.Values[Index];
            }


            return ret;
        }

        public static string TryGetValue(this DynamicCsvRecord This, int ColumnIndex) {
            This.TryGetValue(ColumnIndex, out _, out var tret);

            var ret = tret.Coalesce();

            return ret;
        }

        public static bool TryGetValue(this DynamicCsvRecord This, int ColumnIndex, out string? ColumnHeader, [NotNullWhen(true)] out string? Result) {
            var ret = false;
            Result = default;

            if (ColumnIndex < This.Values.Length) {
                ret = true;
                Result = This.Values[ColumnIndex];
            }

            This.IndexToHeaders.TryGetValue(ColumnIndex, out ColumnHeader);

            return ret;
        }

    }

}
