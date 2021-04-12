using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers {
    public record MessageBuilder : MessageModifier {
        public MessageBuilder(bool? Enabled = default) : base(Enabled) {
        }

        public ImmutableList<IMessageModifier> Actions { get; init; } = ImmutableList<IMessageModifier>.Empty;

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

        public static MessageBuilder Default { get; private set; } = new MessageBuilder(); 

    }
}
