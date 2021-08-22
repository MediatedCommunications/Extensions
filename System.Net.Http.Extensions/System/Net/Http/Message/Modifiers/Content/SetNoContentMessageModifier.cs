using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers
{
    public record SetNoContentMessageModifier : SetContentMessageModifier {
        public SetNoContentMessageModifier(bool? Enabled = default) : base(Enabled) {
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            Message.Content = default;

            return Task.CompletedTask;
        }

    }

}
