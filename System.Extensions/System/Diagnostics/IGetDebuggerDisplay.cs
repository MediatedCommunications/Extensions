namespace System.Diagnostics {
    public interface IGetDebuggerDisplay {
        string GetDebuggerDisplay();
    }

    public interface IGetDebuggerDisplayBuilder : IGetDebuggerDisplay {
        DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder);
    }

    public static class IGetDebuggerDisplayDefaults {
        
        public static string GetDebuggerDisplay(IGetDebuggerDisplayBuilder This) {
            return This.GetDebuggerDisplayBuilder(DisplayBuilder.Create())
                  .GetDebuggerDisplay()
                  ;
        }

        public static DisplayBuilder GetDebuggerDisplayBuilder(IGetDebuggerDisplay This, DisplayBuilder Builder) {
            return Builder
                .Type.Add(This.GetType())
                ;
        }

    }


}
