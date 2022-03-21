using System.Collections.Immutable;
using System.Data.Common;

namespace System.Data {
    public class DataReaderParserContext {
        public ImmutableHashSet<string> ColumnNames { get; }
        public ImmutableList<string> ColumnPositions { get; }
        public DbDataReader DataReader { get; }
        public DataReaderParserContext(DbDataReader DataReader, ImmutableList<string> Columns) {
            this.DataReader = DataReader;
            this.ColumnPositions = Columns;
            this.ColumnNames = Columns.ToImmutableHashSet(StringComparer.InvariantCultureIgnoreCase);
        }
    }

}
