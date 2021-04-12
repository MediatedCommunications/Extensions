namespace System.Net.Http.Message.Modifiers {
    public abstract record SetContentMessageModifier : MessageModifier {
        
        protected SetContentMessageModifier(bool? Enabled = default) : base(Enabled) {
        
        }
    }

}
