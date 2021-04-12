using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static void GetColumnHeaders(this CsvReader This, IDictionary<int, string> OUT_ColumnHeaders) {
            if (This.Read()) {
                for (var i = 0; This.TryGetField<string>(i, out var value); ++i) {
                    OUT_ColumnHeaders[i] = value;
                }
            }
        }

        public static IEnumerable<string[]> AsArrays(string path, IDictionary<int, string>? OUT_ColumnHeaders = default) {
            OUT_ColumnHeaders ??= new SortedDictionary<int, string>();

            var Config = new CsvConfiguration(CultureInfo.InvariantCulture) {
                BadDataFound = default
            };


            using var csv = new CsvReader(new StreamReader(path, Encoding.UTF8), Config);
            
            csv.GetColumnHeaders(OUT_ColumnHeaders);

            var LastHeader = OUT_ColumnHeaders.Keys.Max();
            
            while (csv.Read()) {
                var Values = new LinkedList<string>(); ;

                for (var i = 0; i <= LastHeader; i++) {
                    var Value = i < csv.Context.Parser.Record.Length
                        ? csv.Context.Parser.Record[i]
                        : ""
                        ;
                    Values.Add(Value);
                }

                var ret = Values.ToArray();

                yield return ret;
            }
        }

    }
}
