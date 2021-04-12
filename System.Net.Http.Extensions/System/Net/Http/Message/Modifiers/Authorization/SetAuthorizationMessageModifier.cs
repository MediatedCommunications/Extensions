namespace System.Net.Http.Message.Modifiers {
    public abstract record SetAuthorizationMessageModifier : MessageModifier {
        protected SetAuthorizationMessageModifier(bool? Enabled = default) : base(Enabled) {
        }
    }


}
