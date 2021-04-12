namespace System.Diagnostics {

    [DebuggerDisplay(Debugger2.DebuggerDisplay)]
    public record DisplayRecord : IGetDebuggerDisplayBuilder {
        
        public string GetDebuggerDisplay() {
            return IGetDebuggerDisplayDefaults.GetDebuggerDisplay(this);
        }

        public virtual DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return IGetDebuggerDisplayDefaults.GetDebuggerDisplay(this, Builder);
        }

    }

}
