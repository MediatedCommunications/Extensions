using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Data.SqlClient {
    public record SqlServerHelper : DisplayRecord {
        public string ConnectionString { get; init; } = Strings.Empty;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(ConnectionString)
                ;
        }

        public IAsyncEnumerable<DatabaseJson> ListDatabasesAsync([EnumeratorCancellation] CancellationToken Token = default) {

            return System.Data.QueryParsers.SqlServer.ListAsync(ConnectionString, DatabaseJson.Parser, $@"
                select * from sys.databases
            ", Token);
        }

    }

}
