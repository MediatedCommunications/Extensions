using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using System.Linq;

namespace System.Net.Http.Message.Modifiers
{
    public record SetFormContentMessageModifier : SetContentMessageModifier {
        public ImmutableList<KeyValuePair<string, string>> Content { get; init; }

        public SetFormContentMessageModifier(IEnumerable<KeyValuePair<string, string>> Content, bool? Enabled = null) : base(Enabled) {
            this.Content = Content.ToImmutableList();
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {

            var Query = (
                from x in Content
                let v = KeyValuePair.Create<string?, string?>(x.Key, x.Value)
                select v
                ).ToLinkedList();

            Message.Content = new FormUrlEncodedContent(Query);

            return base.ModifyEnabledAsync(Message);
        }

    }

}
