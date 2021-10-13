using System.Collections.Immutable;
using System.Diagnostics;
using System.Net.Http.Message;
using System.Net.Http.Message.Modifiers;
using System.Threading.Tasks;

namespace System.Net.Http {
    public record HttpRequestMessageBuilder : MessageModifier {
        public HttpRequestMessageBuilder(bool? Enabled = default) : base(Enabled) {
        }

        public ImmutableList<IHttpRequestMessageBuilder> Actions { get; init; } = ImmutableList<IHttpRequestMessageBuilder>.Empty;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add($@"{Actions.Count} {nameof(Actions)}")
                ;
        }

        protected override async Task ModifyEnabledAsync(HttpRequestMessage Message) {

            foreach (var item in Actions) {
                await item.ModifyAsync(Message)
                    .DefaultAwait()
                    ;
            }

        }

    }

}
