using System.Collections.Generic;
using System.Data.Common;
using System.Data.Odbc;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data {
    internal class QueryParser_Odbc : QueryParser {
        public override async Task<DbConnection> ConnectAsync(string ConnectionString, CancellationToken Token = default) {
            var ret = new OdbcConnection(ConnectionString);

            await ret.OpenAsync(Token)
                .DefaultAwait()
                ;


            return ret;
        }
    }

}
