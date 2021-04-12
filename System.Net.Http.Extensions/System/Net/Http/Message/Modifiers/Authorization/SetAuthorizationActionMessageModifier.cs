using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers {
    public record SetAuthorizationActionMessageModifier : SetAuthorizationMessageModifier {
        public Func<HttpRequestMessage, Task> Action { get; init; }

        public SetAuthorizationActionMessageModifier(Func<HttpRequestMessage, Task> Action, bool? Enabled = default) : base(Enabled) {
            this.Action = Action;
        }

        protected override async Task ModifyEnabledAsync(HttpRequestMessage Message) {

            await Action(Message)
                .DefaultAwait()
                ;

        }

    }


}
