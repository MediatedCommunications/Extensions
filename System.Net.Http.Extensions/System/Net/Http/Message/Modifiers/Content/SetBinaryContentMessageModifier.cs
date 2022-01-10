using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers {
    public record SetBinaryContentMessageModifier : SetContentMessageModifier {
        public ArraySegment<byte> Content { get; init; }

        public SetBinaryContentMessageModifier(ArraySegment<byte> Content, bool? Enabled = default) : base(Enabled) {
            this.Content = Content;
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            Message.Content = new ByteArrayContent(Content.ToArray());
            return Task.CompletedTask;
        }

    }

}
