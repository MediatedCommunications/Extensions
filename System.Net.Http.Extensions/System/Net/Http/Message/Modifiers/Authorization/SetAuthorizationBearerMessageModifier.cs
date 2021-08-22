using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers
{
    public record SetAuthorizationBearerMessageModifier : SetAuthorizationValueMessageModifier {

        public SetAuthorizationBearerMessageModifier(string? Value, bool? Enabled = default) : base(Value, Enabled) {
            
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            Message.SetAuthorizationBearer(Value);

            return Task.CompletedTask;
        }

    }

}
