using System.Collections.Generic;

namespace CsvHelper
{
    public static class CsvWriterExtensions {
        public static void WriteCsv<T>(this CsvWriter This, IEnumerable<T> Records) {
            This.WriteHeader<T>();
            This.NextRecord();
            This.WriteRecords(Records);
        }
    }

}
