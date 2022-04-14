using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers
{
    public record SetMultiPartFormContentMessageModifier : SetContentMessageModifier {
        public Func<MultipartFormDataContent> Content { get; init; }

        public SetMultiPartFormContentMessageModifier(IEnumerable<KeyValuePair<string, string>> Content, bool? Enabled = null) : this(CreateContent(Content), Enabled) {
            
        }

        public SetMultiPartFormContentMessageModifier(Action<MultipartFormDataContent> Content, bool? Enabled = null) : this(CreateContent(Content), Enabled) {

        }

        private static Func<MultipartFormDataContent> CreateContent(IEnumerable<KeyValuePair<string, string>> content) {
            var Values = content.ToImmutableArray();

            MultipartFormDataContent CreateContentInternal() {
                var ret = new MultipartFormDataContent();
                foreach (var item in Values) {
                    ret.Add(new StringContent(item.Value), item.Key);
                }


                return ret;
            }

            return CreateContentInternal;
        }

        private static Func<MultipartFormDataContent> CreateContent(Action<MultipartFormDataContent> content) {
            MultipartFormDataContent CreateContentInternal() {
                var ret = new MultipartFormDataContent();
                content(ret);
                return ret;
            }

            return CreateContentInternal;
        }

        public SetMultiPartFormContentMessageModifier(Func<MultipartFormDataContent> Content, bool? Enabled = null) : base(Enabled) {
            this.Content = Content;
        }



        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {

            Message.Content = Content();

            return base.ModifyEnabledAsync(Message);
        }

    }

}
