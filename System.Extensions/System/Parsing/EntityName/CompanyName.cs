using System.Diagnostics;

namespace System {
    public record CompanyName : EntityName {
        public string Name { get; init; } = Strings.Empty;
        public override string Full => Name;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Name)
                ;
        }

    }

}
