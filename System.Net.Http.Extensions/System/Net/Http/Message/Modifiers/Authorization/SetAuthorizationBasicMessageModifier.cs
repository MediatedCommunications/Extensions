using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers
{
    public record SetAuthorizationBasicMessageModifier : SetAuthorizationValueMessageModifier {

        public SetAuthorizationBasicMessageModifier(string? Value, bool? Enabled = default) : base(Value, Enabled) {

        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            Message.SetAuthorizationBasic(Value);

            return Task.CompletedTask;
        }

    }

}
