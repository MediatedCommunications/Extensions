using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers {
    public record SetAuthorizationBearerMessageModifier : SetAuthorizationMessageModifier {
        protected string? Bearer { get; init; }

        public SetAuthorizationBearerMessageModifier(string? Bearer, bool? Enabled = default) : base(Enabled) {
            this.Bearer = Bearer;
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            Message.SetAuthorizationBearer(Bearer);

            return Task.CompletedTask;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Bearer)
                ;
        }

    }

}
