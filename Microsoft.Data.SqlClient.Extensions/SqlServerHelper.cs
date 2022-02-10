using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Microsoft.Data.SqlClient {
    public record SqlServerHelper : DisplayRecord {
        public string ConnectionString { get; init; } = Strings.Empty;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(ConnectionString)
                ;
        }


        protected static IDbConnection SqlConnection(string ConnectionString) {
            var ret = new SqlConnection(ConnectionString);
            ret.Open();
            return ret;
        }

        private static IEnumerable<T> List<T>(Func<IDbConnection> CreateConnection, DataReaderParserFunc<T> Parser, string Query) {
            using var ADS = CreateConnection();

            foreach (var item in List(ADS, Parser, Query)) {
                yield return item;
            }

        }

        private static IEnumerable<T> List<T>(IDbConnection Connection, DataReaderParserFunc<T> Parser, string Query) {


            using var CMD = Connection.CreateCommand();
            CMD.CommandType = CommandType.Text;
            CMD.CommandText = Query;


            var query = DataReaderParser.List(CMD.ExecuteReader, Parser);

            foreach (var item in query) {
                yield return item;
            }

        }


        public IEnumerable<DatabaseJson> ListDatabases() {

            return List(() => SqlConnection(ConnectionString), DatabaseJson.Parser, $@"
    select * from sys.databases
");

        }

    }

}
