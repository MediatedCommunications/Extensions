using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers
{
    public record SetUriMessageModifier : MessageModifier {
        public Uri Uri { get; init; }

        public SetUriMessageModifier(Uri Uri, bool? Enabled = default) : base(Enabled) {
            this.Uri = Uri;
        }

        public SetUriMessageModifier(UriBuilder Uri, bool? Enabled = default) : this(Uri.Uri, Enabled) {

        }

        public SetUriMessageModifier(string Uri, bool? Enabled = default) : this(new Uri(Uri), Enabled) {

        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            Message.RequestUri = Uri;

            return Task.CompletedTask;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Uri)
                ;
        }

    }

}
