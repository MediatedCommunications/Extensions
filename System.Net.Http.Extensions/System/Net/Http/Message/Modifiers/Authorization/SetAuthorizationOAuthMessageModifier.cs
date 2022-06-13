namespace System.Net.Http.Message.Modifiers {
    public record SetAuthorizationOAuthMessageModifier : SetAuthorizationValueMessageModifier {

        public SetAuthorizationOAuthMessageModifier(string? Value, bool? Enabled = default) : base(Value, Enabled) {

        }

        protected override Task ModifyEnabledAsync(HttpRequestMessage Message) {
            Message.SetAuthorizationOauth(Value);

            return Task.CompletedTask;
        }

    }

}
