using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers {
    public record SetMultiPartFormContentMessageModifier : SetContentMessageModifier {
        public Func<MultipartFormDataContent> Content { get; init; }

        public SetMultiPartFormContentMessageModifier(Func<MultipartFormDataContent> Content, bool? Enabled = null) : base(Enabled) {
            this.Content = Content;
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {

            Message.Content = Content();

            return base.ModifyEnabledAsync(Message);
        }

    }

}
