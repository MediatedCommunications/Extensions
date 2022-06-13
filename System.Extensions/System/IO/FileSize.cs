using System.Diagnostics;

namespace System.IO {
    public record FileSize : DisplayRecord {
        public long Byte { get; init; }
        public long Kilo { get; init; } 
        public long Mega { get; init; }
        public long Giga { get; init; }
        public long Tera { get; init; }
        public long Peta { get; init; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.AddExpression(Kilo)
                ;
        }
    }

    public static class FileSizes {
        public static FileSize Default { get; }
        public static FileSize BaseTwo { get; }
        public static FileSize BaseTen { get; }

        static FileSizes() {
            var Ten = 1000;
            var Two = 1024;

            BaseTen = new FileSize
            {
                Byte = 1,
                Kilo = Ten,
                Mega = Ten * Ten ,
                Giga = Ten * Ten * Ten ,
                Tera = Ten * Ten * Ten * Ten ,
                Peta = Ten * Ten * Ten * Ten * Ten,
            };

            BaseTwo = new FileSize
            {
                Byte = 1,
                Kilo = Two,
                Mega = Two * Two,
                Giga = Two * Two * Two,
                Tera = Two * Two * Two * Two,
                Peta = Two * Two * Two * Two * Two,
            };

            Default = BaseTwo;
        }

    }

}
