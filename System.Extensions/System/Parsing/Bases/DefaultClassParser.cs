namespace System {
    public abstract record DefaultClassParser<T> : ClassParser<T> where T : class {
        public abstract T GetValue();
    }


}
