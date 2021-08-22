using System.Text.Json;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Modifiers
{

    public record SetJsonContentMessageModifier : SetContentMessageModifier {
        public object? Content { get; init; }
        public ConfiguredJsonSerializer Serializer { get; init; }

        public SetJsonContentMessageModifier(object? Content, ConfiguredJsonSerializer? Serializer = default, bool? Enabled = default) : base(Enabled) {
            this.Content = Content;
            this.Serializer = Serializer ?? ConfiguredJsonSerializer.Default;
        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            var String = Serializer.Serialize(Content);

            Message.Content = new StringContent(String, Text.Encoding.UTF8, Mime.MediaTypeNames.Application.Json);

            return Task.CompletedTask;
        }

    }

}
