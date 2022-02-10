using System.Diagnostics;

namespace System.Threading {
    public abstract class ThreadPoolThreads : DisplayClass {
        public int Used => Max - Available;
        public abstract int Min { get; set; }
        public abstract int Max { get; set; }
        public abstract int Available { get; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add($@"{Used} Used ({Min} Min <= {Available} Available <= {Max} Max)")
                ;
        }
    }

}
