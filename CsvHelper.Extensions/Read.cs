using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvHelper {

    public static class Read {

        public static LinkedList<T> FromString<T>(string Content, CsvConfiguration? Config = default) {
            var SR = new StringReader(Content);
            var ret = FromTextReader<T>(SR, Config);

            return ret;
        }

        public static LinkedList<T> FromFile<T>(string Path, CsvConfiguration? Config = default) {
            var ret = new LinkedList<T>();
            try {
                using var SR = new StreamReader(Path, Encoding.UTF8);

                ret.Add(FromTextReader<T>(SR, Config));

            } catch (Exception ex) {
                ex.Ignore();
                throw;
            }

            return ret;
        }

        public static LinkedList<T> FromStream<T>(byte[] S, CsvConfiguration? Config = default) {
            return FromStream<T>(S.ToMemoryStream(), Config);
        }

        public static LinkedList<T> FromStream<T>(Stream S, CsvConfiguration? Config = default) {
            var ret = new LinkedList<T>();
            try {
                using var SR = new StreamReader(S, Encoding.UTF8);

                ret.Add(FromTextReader<T>(SR, Config));

            } catch (Exception ex) {
                ex.Ignore();
                throw;
            }

            return ret;
        }

        public static LinkedList<T> FromTextReader<T>(TextReader S, CsvConfiguration? Config = default) {
            var ret = new LinkedList<T>();
            try {
                var MyConfiguration = Config ?? DefaultConfiguration.Create();

                using var csv = new CsvReader(S, MyConfiguration);

                ret.Add(csv.GetRecords<T>());

            } catch (Exception ex) {
                ex.Ignore();
                throw;
            }

            return ret;
        }

        public static DynamicCsvRecord? GetDynamicCsvHeader(this CsvReader This, bool Trim = true) {
            var ret = default(DynamicCsvRecord?);

            if(GetDynamicCsvRecord(This, Trim) is { } tret) {

                var Values = tret.Values;
                
                var IndexToHeaders = Values.WithIndexes().ToImmutableDictionary(x => x.Index, x => x.Item);
                
                var HeadersToIndex = IndexToHeaders
                    .GroupBy(x => x.Value, x => x.Key, StringComparer.InvariantCultureIgnoreCase)
                    .ToImmutableDictionary(x => x.Key, x => x.ToImmutableArray(), StringComparer.InvariantCultureIgnoreCase)
                    ;

                var HeaderToIndex = HeadersToIndex
                    .ToImmutableDictionary(x => x.Key, x => x.Value.First(), StringComparer.InvariantCultureIgnoreCase)
                    ;

                ret = new DynamicCsvRecord()
                {
                    Values = Values,
                    HeadersToIndex = HeadersToIndex,
                    HeaderToIndex = HeaderToIndex,
                    IndexToHeaders = IndexToHeaders,
                };
            }

            return ret;
        }

        public static DynamicCsvRecord? GetDynamicCsvRecord(this CsvReader This, bool Trim = true) {
            var ret = default(DynamicCsvRecord?);

            if (This.Read()) {
                var Values = new List<string>();
                for (var i = 0; i < This.Parser.Record.Length; ++i) {
                    var value = This.Parser.Record[i];
                    if (Trim) {
                        value = value.TrimSafe();
                    }

                    Values.Add(value);
                }

                ret = new DynamicCsvRecord()
                {
                    Values = Values.ToImmutableArray(),
                };

            }

            return ret;
        }


        public static IEnumerable<DynamicCsvRecord> GetDynamicRecords(this CsvReader This, bool Include_Column_Headers = true, bool Trim = true) {

            var Header = default(DynamicCsvRecord?);
            
            if (Include_Column_Headers) {
                Header = GetDynamicCsvHeader(This, Trim);
            }
            
            while (GetDynamicCsvRecord(This, Trim) is { } V1) {

                var ret = new DynamicCsvRecord() {
                    IndexToHeaders = Header?.IndexToHeaders ?? V1.IndexToHeaders,
                    HeadersToIndex = Header?.HeadersToIndex ?? V1.HeadersToIndex,
                    HeaderToIndex = Header?.HeaderToIndex ?? V1.HeaderToIndex,
                    Values = V1.Values,
                };

                yield return ret;

            }


        }

    }

}
