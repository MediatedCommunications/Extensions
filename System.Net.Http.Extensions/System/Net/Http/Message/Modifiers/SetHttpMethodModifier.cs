using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers {
    public record SetHttpMethodModifier : MessageModifier {
        public HttpMethod Method { get; init; }

        public SetHttpMethodModifier(HttpMethod Method, bool? Enabled = default) : base(Enabled) {
            this.Method = Method;
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            Message.Method = Method;

            return Task.CompletedTask;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Method)
                ;
        }

    }

}
