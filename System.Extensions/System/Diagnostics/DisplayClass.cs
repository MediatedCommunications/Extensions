namespace System.Diagnostics {

    [Serializable]
    [DebuggerDisplay(Debugger2.GetDebuggerDisplay)]
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
