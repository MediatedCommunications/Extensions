namespace System {
    public abstract record DefaultClassParser<T> : ClassParser<T> where T : class {
        protected abstract T GetDefaultValue();

        public T GetValue(string? Input) {
            return GetValue(Input, GetDefaultValue());
        }
    }


}
