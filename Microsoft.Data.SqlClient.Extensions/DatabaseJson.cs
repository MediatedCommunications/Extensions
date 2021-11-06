using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Data.SqlClient {

    public record DatabaseJson : DisplayRecord {
        public long Id { get; init; }
        public string Name { get; init; } = string.Empty;

        public SqlDatabaseType Type { get; init; }
        
        public DateTime Created_At { get; init; }
        public SqlDatabaseCompatibilityLevel Compatibility { get; init; }

        public string Collation { get; init; } = string.Empty;
        public SqlDatabaseRecoveryModel RecoveryModel { get; init; }
        public SqlDatabaseStatus Status { get; init; }

        public DatabaseFlagsJson Flags { get; init; } = new();

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Id.Add(Id)
                .Type.Add(Type)
                .Data.Add(Name)
                .Status.Add(Status)
                ;
        }


        static internal Dictionary<string, SqlDatabaseType> DatabaseTypes = new Dictionary<string, SqlDatabaseType>(StringComparer.InvariantCultureIgnoreCase) {
            ["master"] = SqlDatabaseType.Master,
            ["model"] = SqlDatabaseType.Model,
            ["tempdb"] = SqlDatabaseType.TempDb,
            ["msdb"] = SqlDatabaseType.MsDb,
        };

        static internal DatabaseJson? Parser(DataReaderParserContext Context) {
            var Id = Context.DataReader["Database_Id"].ToString().Parse().AsLong().GetValue();
            var Name = Context.DataReader["Name"].ToString().Parse().AsString().GetValue();
            var Type = DatabaseTypes.TryGetValue(Name) ?? SqlDatabaseType.User;

            var Collation = Context.DataReader["Collation_Name"].ToString().Parse().AsString().GetValue();
            var Compatibility = Context.DataReader["Compatibility_Level"].ToString().Parse().AsEnum<SqlDatabaseCompatibilityLevel>().GetValue();
            var RecoveryModel = Context.DataReader["Recovery_Model"].ToString().Parse().AsEnum<SqlDatabaseRecoveryModel>().GetValue();
            var Status = Context.DataReader["State"].ToString().Parse().AsEnum<SqlDatabaseStatus>().GetValue();
            var Created_At = Context.DataReader["Create_Date"].ToString().Parse().AsDateTime().GetValue();

            

            var ret = new DatabaseJson() {
                Id = Id,
                Name = Name,
                Type = Type,
                Collation = Collation,
                Compatibility = Compatibility,
                Created_At = Created_At,
                RecoveryModel = RecoveryModel,
                Status = Status,

                Flags = new() {
                    AutoClose = Context.DataReader["is_auto_close_on"].ToString().Parse().AsBool().GetValue(),
                    AutoShrink = Context.DataReader["is_auto_shrink_on"].ToString().Parse().AsBool().GetValue(),
                    AutoCreateStats = Context.DataReader["is_auto_create_stats_on"].ToString().Parse().AsBool().GetValue(),
                    AutoCreateStatsIncremental = Context.DataReader["is_auto_create_stats_incremental_on"].ToString().Parse().AsBool().GetValue(),
                    AutoUpdateStats = Context.DataReader["is_auto_update_stats_on"].ToString().Parse().AsBool().GetValue(),
                    AutoUpdateStatsAsync = Context.DataReader["is_auto_update_stats_async_on"].ToString().Parse().AsBool().GetValue(),
                },
            };

            return ret;
        }

    }

    public static class DatabaseExtensions {

    }

    [Flags]
    public enum SqlDatabaseType {
        Unknown = 0,
        User = 0b0001_0000,
        System = 0b0010_0000,
        Master = System | 0b0001,
        Model = System | 0b0001,
        MsDb = System | 0b0100,
        TempDb = System | 0b1000,
    }

}
