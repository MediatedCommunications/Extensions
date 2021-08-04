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

        public static IEnumerable<DynamicCsvRecord> DynamicRecords(this CsvReader This, bool Include_Column_Headers = true, bool Trim = true) {
            var IndexToHeaders = ImmutableDictionary<int, string>.Empty;

            if (Include_Column_Headers) {
                var tret = new Dictionary<int, string>();
                if (This.Read()) {
                    for (var i = 0; i < This.Parser.Record.Length; ++i) {
                        var value = This.Parser.Record[i];
                        if (Trim) {
                            value = value.TrimSafe();
                        }
                        
                        tret[i] = value;
                    }
                }
                IndexToHeaders = tret.ToImmutableDictionary();
            }
            
            var HeadersToIndex = IndexToHeaders
                .GroupBy(x => x.Value, x => x.Key, StringComparer.InvariantCultureIgnoreCase)
                .ToImmutableDictionary(x => x.Key, x => x.ToImmutableList(), StringComparer.InvariantCultureIgnoreCase)
                ;

            var HeaderToIndex = HeadersToIndex
                .ToImmutableDictionary(x => x.Key, x => x.Value.First(), StringComparer.InvariantCultureIgnoreCase);


            while (This.Read()) {
                var tret = new List<string>();
                for (var i = 0; i < This.Parser.Record.Length; ++i) {
                    var value = This.Parser.Record[i];

                    if (Trim) {
                        value = value.TrimSafe();
                    }

                    tret.Add(value);
                }
                var Values = tret.ToImmutableList();

                var ret = new DynamicCsvRecord() {
                    IndexToHeaders = IndexToHeaders,
                    HeadersToIndex = HeadersToIndex,
                    HeaderToIndex = HeaderToIndex,
                    Values = Values,
                };

                yield return ret;

            }


        }

    }

}
