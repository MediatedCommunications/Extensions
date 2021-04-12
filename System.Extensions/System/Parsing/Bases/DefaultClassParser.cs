namespace System {
    public abstract record DefaultClassParser<T> : ClassParser<T> where T : class {
        public DefaultClassParser(string? Value) : base(Value) {
        
        }

        public abstract T GetValue();
    }


}
