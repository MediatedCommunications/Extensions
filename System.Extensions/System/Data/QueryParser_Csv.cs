using System.Data.Common;
using CsvHelper;
using System.Runtime.CompilerServices;

namespace System.Data {
    internal class QueryParser_Csv : QueryParser {
        public override async Task<DbConnection> ConnectAsync(string ConnectionString, CancellationToken Token = default) {
            throw new NotImplementedException();
        }

        public override IAsyncEnumerable<T> ListAsync<T>(DbConnection Connection, DataReaderParserFunc<T> Parser, string Query, [EnumeratorCancellation] CancellationToken Token = default) {
            throw new NotImplementedException();
        }

        public override Task<int> NonQueryAsync(string ConnectionString, string Query, CommandType CommandType = CommandType.Text, CancellationToken Token = default) {
            throw new NotImplementedException();
        }

        public override async IAsyncEnumerable<T> ListAsync<T>(string ConnectionString, DataReaderParserFunc<T> Parser, string Query, [EnumeratorCancellation] CancellationToken Token = default) {

            using var TR = new System.IO.StreamReader(ConnectionString);
            
            var Reader = new CsvHelper.CsvReader(TR, System.Globalization.CultureInfo.InvariantCulture);
            var DR = new CsvHelper.CsvDataReader(Reader);

            var tret = DataReaderParser.ListAsync(DR, Parser, Token)
                .DefaultAwait()
                ;

            await foreach (var ret in tret) {
                yield return ret;
            }

        }

    }


}
