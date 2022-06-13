namespace System.Diagnostics {
    public interface IGetDebuggerDisplayBuilder : IGetDebuggerDisplay {
        DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder);
    }


}
