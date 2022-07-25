using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data {

    public delegate T? DataReaderParserFunc<T>(DataReaderParserContext Context);

    public static class DataReaderParser {

        public static async IAsyncEnumerable<T> ListAsync<T>(Func<IDataReader> Source, DataReaderParserFunc<T> Parser, [EnumeratorCancellation] CancellationToken Token = default) {
            using var DR = Source();

            var query = ListAsync(DR, Parser, Token)
                .DefaultAwait()
                ;

            await foreach (var item in query) {
                yield return item;
            }

        }

        public static async IAsyncEnumerable<T> ListAsync<T>(IDataReader DR, DataReaderParserFunc<T> Parser, [EnumeratorCancellation] CancellationToken Token = default) {
            var Columns = DR.GetColumnNames();

            var Context = new DataReaderParserContext(DR, Columns);

            Func<Task<bool>> ReadAsync = () =>
            {
                var ret = DR.Read();
                return Task.FromResult(ret);
            };
            
            if(DR is DbDataReader V1) {
                ReadAsync = () => V1.ReadAsync();
            }



            while (await ReadAsync().DefaultAwait()) {
                var tret = Parser(Context);

                if (tret is { }) {
                    yield return tret;
                }

                if (Token.ShouldStop()) {
                    yield break;
                }

            }
        }
    }

}
