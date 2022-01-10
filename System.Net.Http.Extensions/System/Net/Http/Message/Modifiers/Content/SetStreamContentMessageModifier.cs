using System.IO;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers {
    public record SetStreamContentMessageModifier : SetContentMessageModifier {
        public Func<Stream> Content { get; init; }

        public SetStreamContentMessageModifier(Func<Stream> Content, bool? Enabled = default) : base(Enabled) {
            this.Content = Content;
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            Message.Content = new StreamContent(Content());
            return Task.CompletedTask;
        }

    }

}
