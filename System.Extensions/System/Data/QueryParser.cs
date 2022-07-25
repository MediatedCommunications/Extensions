using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data {

    public abstract class QueryParser : DisplayClass {
        private static int DefaultCommandTimeout { get; } = 60 * 60;

        public abstract Task<DbConnection> ConnectAsync(string ConnectionString, CancellationToken Token = default);

        public virtual async Task<int> NonQueryAsync(string ConnectionString, string Query, CommandType CommandType = CommandType.Text, CancellationToken Token = default) {
            using var V1 = await ConnectAsync(ConnectionString, Token)
                .DefaultAwait()
                ;

            using var V2 = V1.CreateCommand();

            V2.CommandType = CommandType;
            V2.CommandText = Query;
            V2.CommandTimeout = DefaultCommandTimeout;

            var ret = await V2.ExecuteNonQueryAsync(Token)
                .DefaultAwait()
                ;

            return ret;

        }


        public virtual async IAsyncEnumerable<T> ListAsync<T>(string ConnectionString, DataReaderParserFunc<T> Parser, string Query, [EnumeratorCancellation] CancellationToken Token = default) {

            using var Connection = await ConnectAsync(ConnectionString, Token)
                .DefaultAwait()
                ;

            await foreach (var ret in ListAsync(Connection, Parser, Query).DefaultAwait()) {
                yield return ret;
            }

        }

        public virtual async IAsyncEnumerable<T> ListAsync<T>(DbConnection Connection, DataReaderParserFunc<T> Parser, string Query, [EnumeratorCancellation] CancellationToken Token = default) {

            using var CMD = Connection.CreateCommand();
            CMD.CommandType = CommandType.Text;
            CMD.CommandText = Query;
            CMD.CommandTimeout = DefaultCommandTimeout;

            var query = DataReaderParser.ListAsync(CMD.ExecuteReader, Parser, Token)
                .DefaultAwait()
                ;

            await foreach (var item in query) {
                yield return item;
            }
        }


    }

}
