using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data {
    public class DataReaderParserContext {
        public ImmutableHashSet<string> Columns { get; }
        public IDataReader DataReader { get; }
        public DataReaderParserContext(IDataReader DataReader, IEnumerable<string> Columns) {
            this.DataReader = DataReader;
            this.Columns = Columns.ToImmutableHashSet(StringComparer.InvariantCultureIgnoreCase);
        }
    }

    public delegate T? DataReaderParserFunc<T>(DataReaderParserContext Context);

    public static class DataReaderParser {

        public static IEnumerable<T> List<T>(Func<IDbCommand> Source, DataReaderParserFunc<T> Parser) {
            using var CMD = Source();

            var query = List(CMD.ExecuteReader, Parser);

            foreach (var item in query) {
                yield return item;
            }


        }

        public static IEnumerable<T> List<T>(Func<IDataReader> Source, DataReaderParserFunc<T> Parser) {
            using var DR = Source();

            var query = List(DR, Parser);

            foreach (var item in query) {
                yield return item;
            }

        }

        public static IEnumerable<T> List<T>(IDataReader DR, DataReaderParserFunc<T> Parser) {
            var Columns = DR.GetColumnNames();

            var Context = new DataReaderParserContext(DR, Columns);

            while (DR.Read()) {
                var tret = Parser(Context);
                if (tret is { }) {
                    yield return tret;
                }
            }
        }
    }

}
