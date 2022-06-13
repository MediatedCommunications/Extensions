namespace System.Diagnostics {

    [Serializable]
    [DebuggerDisplay(Debugger2.GetDebuggerDisplay)]
    public record DisplayRecord : IGetDebuggerDisplayBuilder {
        
        public string GetDebuggerDisplay() {
            return IGetDebuggerDisplayDefaults.GetDebuggerDisplay(this);
        }

        public virtual DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return IGetDebuggerDisplayDefaults.GetDebuggerDisplayBuilder(this, Builder);
        }

    }

}
