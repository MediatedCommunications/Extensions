namespace System.Diagnostics {

    [DebuggerDisplay(Debugger2.DebuggerDisplay)]
    public class DisplayClass : IGetDebuggerDisplayBuilder {

        public override string ToString() {
            return GetDebuggerDisplay();
        }

        public string GetDebuggerDisplay() {
            return IGetDebuggerDisplayDefaults.GetDebuggerDisplay(this);
        }

        public virtual DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return IGetDebuggerDisplayDefaults.GetDebuggerDisplayBuilder(this, Builder);
        }
    }

}
