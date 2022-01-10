using System.Diagnostics;

namespace System.Threading {
    public abstract class ThreadPoolThreads : DisplayClass {
        public int Current => Max - Available;
        public abstract int Min { get; set; }
        public abstract int Max { get; set; }
        public abstract int Available { get; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add($@"{Min} Min <= {Current} Current <= {Max} Max")
                ;
        }
    }

}
