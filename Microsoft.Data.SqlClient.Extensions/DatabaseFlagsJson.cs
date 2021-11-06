using System.Diagnostics;

namespace Microsoft.Data.SqlClient { 
    public record DatabaseFlagsJson : DisplayRecord {
        public bool AutoClose { get; init; }
        public bool AutoShrink { get; init; }
        public bool AutoCreateStats { get; init; }
        public bool AutoCreateStatsIncremental { get; init; }
        public bool AutoUpdateStats { get; init; }
        public bool AutoUpdateStatsAsync { get; init; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.AddFlag(AutoClose)
                .Data.AddFlag(AutoShrink)
                ;
        }

    }

}
