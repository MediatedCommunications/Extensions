using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers
{
    public abstract record MessageModifier : DisplayRecord, IMessageModifier {
        public bool Enabled { get; init; }

        public MessageModifier(bool? Enabled = default) {
            this.Enabled = Enabled ?? true;
        }

        public virtual async Task ModifyAsync(HttpRequestMessage Message) {
            if (Enabled) {
                await ModifyEnabledAsync(Message)
                    .DefaultAwait()
                    ;
            } else {
                await ModifyDisabledAsync(Message)
                    .DefaultAwait()
                    ;
            }
        }

        protected virtual Task ModifyEnabledAsync(HttpRequestMessage Message) {
            return Task.CompletedTask;
        }

        protected virtual Task ModifyDisabledAsync(HttpRequestMessage Message) {
            return Task.CompletedTask;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Status.IsEnabled(Enabled)
                ;
        }

    }


}
