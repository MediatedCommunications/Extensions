using System.Diagnostics;

namespace System.Net.Http.Message.Modifiers {
    public abstract record SetAuthorizationValueMessageModifier : SetAuthorizationMessageModifier {
        protected string? Value { get; init; }

        public SetAuthorizationValueMessageModifier(string? Value, bool? Enabled = default) : base(Enabled) {
            this.Value = Value;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Value)
                ;
        }

    }

}
