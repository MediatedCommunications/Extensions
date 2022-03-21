using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data {
    internal class QueryParser_SqlServer : QueryParser {
        public override async Task<DbConnection> ConnectAsync(string ConnectionString, CancellationToken Token = default) {
            var ret = new SqlConnection(ConnectionString);

            await ret.OpenAsync(Token)
                .DefaultAwait()
                ;

            return ret;
        }
    }

}
